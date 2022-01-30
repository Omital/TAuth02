using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using System.Threading.Tasks;
using TAuth02.Authorization.Roles;

namespace TAuth02.Authorization.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimStore,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimStore,
                userOrganizationUnitRepository,
                organizationUnitRoleRepository)
        {
        }

        public override User FindById(long userId)
        {
            return base.FindById(userId);
        }

        public override Task<User> FindByIdAsync(long userId)
        {
            return base.FindByIdAsync(userId);
        }
    }
}