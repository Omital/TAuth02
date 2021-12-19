using System;
using System.Configuration;
using Abp.Owin;
using TAuth02.Api.Controllers;
using TAuth02.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using Abp.Dependency;
using Abp.Runtime.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace TAuth02.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            app.CreatePerOwinContext(IocManager.Instance.Resolve<Authorization.Users.UserManager>);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = context => SecurityStampValidator.OnValidateIdentity<Authorization.Users.UserManager, Authorization.Users.User, long>(
                    validateInterval: TimeSpan.FromSeconds(5),
                    regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user,context.Identity, DefaultAuthenticationTypes.ApplicationCookie),
                    getUserIdCallback: claimsIdentity => ClaimsIdentityExtensions.GetUserId(claimsIdentity).Value).Invoke(context),
                },
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                // evaluate for Persistent cookies (IsPermanent == true). Defaults to 14 days when not set.
                ExpireTimeSpan = new TimeSpan(0, 0, int.Parse(ConfigurationManager.AppSettings["Cookie.ExpireTimeSpan.Minute"] ?? "10"), 0),
                SlidingExpiration = true
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.MapSignalR();
        }
    }
}
