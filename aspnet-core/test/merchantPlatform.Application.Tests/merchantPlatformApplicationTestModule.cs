using Volo.Abp.Modularity;

namespace merchantPlatform;

[DependsOn(
    typeof(merchantPlatformApplicationModule),
    typeof(merchantPlatformDomainTestModule)
)]
public class merchantPlatformApplicationTestModule : AbpModule
{

}
