using merchantPlatform.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace merchantPlatform.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class merchantPlatformController : AbpControllerBase
{
    protected merchantPlatformController()
    {
        LocalizationResource = typeof(merchantPlatformResource);
    }
}
