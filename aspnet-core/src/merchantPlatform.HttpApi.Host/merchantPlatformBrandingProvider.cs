using Microsoft.Extensions.Localization;
using merchantPlatform.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace merchantPlatform;

[Dependency(ReplaceServices = true)]
public class merchantPlatformBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<merchantPlatformResource> _localizer;

    public merchantPlatformBrandingProvider(IStringLocalizer<merchantPlatformResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
