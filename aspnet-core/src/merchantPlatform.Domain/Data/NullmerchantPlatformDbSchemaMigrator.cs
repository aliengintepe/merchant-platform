using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace merchantPlatform.Data;

/* This is used if database provider does't define
 * ImerchantPlatformDbSchemaMigrator implementation.
 */
public class NullmerchantPlatformDbSchemaMigrator : ImerchantPlatformDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
