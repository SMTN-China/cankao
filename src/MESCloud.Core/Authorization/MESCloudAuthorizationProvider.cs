using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MESCloud.Authorization
{
    public class MESCloudAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Menus, L("Menus"));
            context.CreatePermission(PermissionNames.Pages_Orgs, L("Orgs"));
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_I18N, L("I18N"));
            

            context.CreatePermission(PermissionNames.Pages_BOMs, L("BOMs"));
            context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"));
            context.CreatePermission(PermissionNames.Pages_Lines, L("Lines"));
            context.CreatePermission(PermissionNames.Pages_StorageLocations, L("StorageLocations"));
            context.CreatePermission(PermissionNames.Pages_Storages, L("Storages"));
            context.CreatePermission(PermissionNames.Pages_StorageArea, L("StorageArea"));
            context.CreatePermission(PermissionNames.Pages_MPNs, L("MPNs"));
            context.CreatePermission(PermissionNames.Pages_StorageLocationTypes, L("StorageLocationTypes"));
            context.CreatePermission(PermissionNames.Pages_BarCodeAnalysis, L("BarCodeAnalysis"));
            context.CreatePermission(PermissionNames.Pages_Reels, L("Reels"));
            context.CreatePermission(PermissionNames.Pages_Slots, L("Slots"));
            context.CreatePermission(PermissionNames.Pages_WorkBills, L("WorkBills"));
            context.CreatePermission(PermissionNames.Pages_ReadyMBills, L("ReadyMBills"));
            context.CreatePermission(PermissionNames.Pages_ReelMoveMethods, L("ReelMoveMethods"));
            context.CreatePermission(PermissionNames.Pages_ReadyMBillDetaileds, L("ReadyMBillDetaileds"));
            context.CreatePermission(PermissionNames.Pages_ReelMoveLogs, L("ReelMoveLogs"));
            context.CreatePermission(PermissionNames.Pages_ReceivedReelBills, L("ReceivedReelBills"));
            context.CreatePermission(PermissionNames.Pages_UPHs, L("UPHs"));

            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {           
            return new LocalizableString(name, MESCloudConsts.LocalizationSourceName);
        }
    }
}
