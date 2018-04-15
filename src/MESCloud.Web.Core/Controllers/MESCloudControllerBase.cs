using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MESCloud.Controllers
{
    public abstract class MESCloudControllerBase: AbpController
    {
        protected MESCloudControllerBase()
        {
            LocalizationSourceName = MESCloudConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
