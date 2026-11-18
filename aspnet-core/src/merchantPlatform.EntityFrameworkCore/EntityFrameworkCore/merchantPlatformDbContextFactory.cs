using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace merchantPlatform.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class merchantPlatformDbContextFactory : IDesignTimeDbContextFactory<merchantPlatformDbContext>
{
    public merchantPlatformDbContext CreateDbContext(string[] args)
    {
        merchantPlatformEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<merchantPlatformDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new merchantPlatformDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../merchantPlatform.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
