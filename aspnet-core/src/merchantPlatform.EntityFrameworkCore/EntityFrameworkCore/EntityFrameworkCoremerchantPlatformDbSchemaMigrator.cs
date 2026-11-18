using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using merchantPlatform.Data;
using Volo.Abp.DependencyInjection;

namespace merchantPlatform.EntityFrameworkCore;

public class EntityFrameworkCoremerchantPlatformDbSchemaMigrator
    : ImerchantPlatformDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoremerchantPlatformDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the merchantPlatformDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<merchantPlatformDbContext>()
            .Database
            .MigrateAsync();
    }
}
