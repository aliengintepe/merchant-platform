using Volo.Abp.Modularity;

namespace merchantPlatform;

[DependsOn(
    typeof(merchantPlatformDomainModule),
    typeof(merchantPlatformTestBaseModule)
)]
public class merchantPlatformDomainTestModule : AbpModule
{

}
