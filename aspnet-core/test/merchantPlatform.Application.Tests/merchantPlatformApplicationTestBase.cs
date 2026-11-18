using Volo.Abp.Modularity;

namespace merchantPlatform;

public abstract class merchantPlatformApplicationTestBase<TStartupModule> : merchantPlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
