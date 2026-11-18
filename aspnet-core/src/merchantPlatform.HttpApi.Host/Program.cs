using merchantPlatform.IServices;
using merchantPlatform.ML.Models;
using merchantPlatform.ML.Trainers;
using merchantPlatform.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace merchantPlatform;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting merchantPlatform.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<merchantPlatformHttpApiHostModule>();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var trainer = scope.ServiceProvider.GetRequiredService<DocumentClassificationTrainer>();
                var docService = scope.ServiceProvider.GetRequiredService<DocumentClassificationService>();

                // Dosya yolu, projenizin kök dizininden başlar
                var trainingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ML", "Data", "training.csv");
                if (File.Exists(trainingFilePath))
                {
                    var model = trainer.TrainFromCsv(trainingFilePath);
                    docService.SetModel(model);
                    Log.Information("✅ ML modeli CSV dosyasından başarıyla eğitildi ve yüklendi.");
                }
                else
                {
                    Log.Error($"❌ Eğitim dosyası bulunamadı: {trainingFilePath}. ML modeli tahmin yapamayacaktır.");
                }
            }
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
