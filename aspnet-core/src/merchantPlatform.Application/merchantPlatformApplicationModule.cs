using merchantPlatform.IServices;
using merchantPlatform.ML.Models;
using merchantPlatform.ML.Trainers;
using merchantPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace merchantPlatform;

[DependsOn(
    typeof(merchantPlatformDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(merchantPlatformApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class merchantPlatformApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)

    {
        context.Services.AddTransient<IOcrService, OcrService>();
        context.Services.AddTransient<IOcrResultService, OcrResultService>();

        context.Services.AddSingleton<MLContext>();

        context.Services.AddSingleton<DocumentClassificationTrainer>();
        context.Services.AddSingleton<DocumentClassificationService>();


        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<merchantPlatformApplicationModule>();
           
        });
       



    }
}
