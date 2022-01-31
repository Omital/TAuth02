using Abp.Domain.Uow;
using System;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace TAuth02.Authorization.Users
{
    public static class CustomSecurityStampValidator
    {
        /// <summary>
        ///     Can be used as the ValidateIdentity method for a CookieAuthenticationProvider which will check a user's security
        ///     stamp after validateInterval
        ///     Rejects the identity if the stamp changes, and otherwise will call regenerateIdentity to sign in a new
        ///     ClaimsIdentity
        /// </summary>
        /// <typeparam name="TManager"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="validateInterval"></param>
        /// <param name="regenerateIdentity"></param>
        /// <returns></returns>
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(
            TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity)
            where TManager : UserManager<TUser, string>
            where TUser : class, IUser<string>
        {
            return OnValidateIdentity(validateInterval, regenerateIdentity, id => id.GetUserId());
        }

        /// <summary>
        ///     Can be used as the ValidateIdentity method for a CookieAuthenticationProvider which will check a user's security
        ///     stamp after validateInterval
        ///     Rejects the identity if the stamp changes, and otherwise will call regenerateIdentity to sign in a new
        ///     ClaimsIdentity
        /// </summary>
        /// <typeparam name="TManager"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="validateInterval"></param>
        /// <param name="regenerateIdentityCallback"></param>
        /// <param name="getUserIdCallback"></param>
        /// <returns></returns>
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(
            TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback,
            Func<ClaimsIdentity, TKey> getUserIdCallback)
            where TManager : UserManager<TUser, TKey>
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (getUserIdCallback == null)
            {
                throw new ArgumentNullException("getUserIdCallback");
            }
            return async context =>
            {
                var uowm = Abp.Dependency.IocManager.Instance.Resolve<IUnitOfWorkManager>();
                var tenantId = context.Identity.Claims.Cast<Claim>().FirstOrDefault(p => p.Type == Abp.Runtime.Security.AbpClaimTypes.TenantId)?.Value;
                
                await uowm.WithUnitOfWorkAsync(async() =>
                 {
                     using (uowm.Current.SetTenantId(string.IsNullOrEmpty(tenantId) ? (int?)null : Convert.ToInt32(tenantId)))
                     {
                         var currentUtc = DateTimeOffset.UtcNow;
                         if (context.Options != null && context.Options.SystemClock != null)
                         {
                             currentUtc = context.Options.SystemClock.UtcNow;
                         }
                         var issuedUtc = context.Properties.IssuedUtc;

                        // Only validate if enough time has elapsed
                        var validate = (issuedUtc == null);
                         if (issuedUtc != null)
                         {
                             var timeElapsed = currentUtc.Subtract(issuedUtc.Value);
                             validate = timeElapsed > validateInterval;
                         }
                         if (validate)
                         {
                             var manager = context.OwinContext.GetUserManager<TManager>();
                             var userId = getUserIdCallback(context.Identity);
                             if (manager != null && userId != null)
                             {
                                 var user = await manager.FindByIdAsync(userId);
                                 var reject = true;
                                // Refresh the identity if the stamp matches, otherwise reject
                                if (user != null && manager.SupportsUserSecurityStamp)
                                 {
                                     var securityStamp =
                                         context.Identity.FindFirstValue(Constants.DefaultSecurityStampClaimType);
                                     if (securityStamp == await manager.GetSecurityStampAsync(userId))
                                     {
                                         reject = false;
                                        // Regenerate fresh claims if possible and resign in
                                        if (regenerateIdentityCallback != null)
                                         {
                                             var identity = await regenerateIdentityCallback.Invoke(manager, user);
                                             if (identity != null)
                                             {
                                                // Fix for regression where this value is not updated
                                                // Setting it to null so that it is refreshed by the cookie middleware
                                                context.Properties.IssuedUtc = null;
                                                 context.Properties.ExpiresUtc = null;
                                                 context.OwinContext.Authentication.SignIn(context.Properties, identity);
                                             }
                                         }
                                     }
                                 }
                                 if (reject)
                                 {
                                     context.RejectIdentity();
                                     context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
                                 }
                             }
                         }
                     }
                 });

            };
        }
    }
}

