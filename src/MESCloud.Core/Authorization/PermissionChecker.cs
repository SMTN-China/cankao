using Abp.Authorization;
using MESCloud.Authorization.Roles;
using MESCloud.Authorization.Users;

namespace MESCloud.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
