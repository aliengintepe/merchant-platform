using System.Threading.Tasks;

namespace merchantPlatform.Data;

public interface ImerchantPlatformDbSchemaMigrator
{
    Task MigrateAsync();
}
