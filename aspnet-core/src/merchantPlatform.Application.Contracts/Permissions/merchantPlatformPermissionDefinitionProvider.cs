using merchantPlatform.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace merchantPlatform.Permissions;

public class merchantPlatformPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(merchantPlatformPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(merchantPlatformPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<merchantPlatformResource>(name);
    }
}
