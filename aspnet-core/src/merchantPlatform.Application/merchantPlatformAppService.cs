using System;
using System.Collections.Generic;
using System.Text;
using merchantPlatform.Localization;
using Volo.Abp.Application.Services;

namespace merchantPlatform;

/* Inherit your application services from this class.
 */
public abstract class merchantPlatformAppService : ApplicationService
{
    protected merchantPlatformAppService()
    {
        LocalizationResource = typeof(merchantPlatformResource);
    }
}
