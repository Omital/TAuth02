using Abp.Authorization;
using TAuth02.Authorization.Roles;
using TAuth02.Authorization.Users;

namespace TAuth02.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
