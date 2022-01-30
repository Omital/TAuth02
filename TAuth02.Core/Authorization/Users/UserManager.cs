using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TAuth02.Authorization.Roles;

namespace TAuth02.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                  userStore,
                  roleManager,
                  permissionManager,
                  unitOfWorkManager,
                  cacheManager,
                  organizationUnitRepository,
                  userOrganizationUnitRepository,
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, ClaimsIdentity CurrentIdentity, string authenticationType)
        {
            
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await base.CreateIdentityAsync(user, authenticationType);

            int? tennantId = null;
            int? impersonateTenantId = null;
            long? impersonateUserId = null;

            var _tennantId = CurrentIdentity.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
            if (_tennantId != null && _tennantId.Value != null)
                tennantId = Convert.ToInt32(_tennantId.Value);

            var _impersonateTenantId = CurrentIdentity.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
            if (_impersonateTenantId != null && _impersonateTenantId.Value != null)
                impersonateTenantId = Convert.ToInt32(_impersonateTenantId.Value);

            var _impersonateUserId = CurrentIdentity.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
            if (_impersonateUserId != null && _impersonateUserId.Value != null)
                impersonateUserId = Convert.ToInt64(_impersonateUserId.Value);


            if (tennantId.HasValue && userIdentity.Claims.Any(p => p.Type == AbpClaimTypes.TenantId) == false)
                userIdentity.AddClaim(new Claim(AbpClaimTypes.TenantId, tennantId.ToString()));

            if (impersonateTenantId.HasValue && userIdentity.Claims.Any(p => p.Type == AbpClaimTypes.ImpersonatorTenantId) == false)
                userIdentity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId, impersonateTenantId.ToString()));

            if (impersonateUserId.HasValue && userIdentity.Claims.Any(p => p.Type == AbpClaimTypes.ImpersonatorUserId) == false)
                userIdentity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId, impersonateUserId.ToString()));

            return userIdentity;
        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {

            var idntities = await base.CreateIdentityAsync(user, authenticationType);

            return idntities;
        }

    }
}