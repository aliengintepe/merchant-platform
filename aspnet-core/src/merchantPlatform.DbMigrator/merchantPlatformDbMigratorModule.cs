using merchantPlatform.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace merchantPlatform.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(merchantPlatformEntityFrameworkCoreModule),
    typeof(merchantPlatformApplicationContractsModule)
    )]
public class merchantPlatformDbMigratorModule : AbpModule
{
}
