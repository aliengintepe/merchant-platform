using Volo.Abp.Modularity;

namespace merchantPlatform;

/* Inherit from this class for your domain layer tests. */
public abstract class merchantPlatformDomainTestBase<TStartupModule> : merchantPlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
