using Volo.Abp.Settings;

namespace merchantPlatform.Settings;

public class merchantPlatformSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(merchantPlatformSettings.MySetting1));
    }
}
