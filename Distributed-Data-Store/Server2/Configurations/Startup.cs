using Server2.BackgroundTask;
using Server2.Repositories.DataStorage;
using Server2.Repositories.SharedStorage;
using Server2.Services.DataService;
using Server2.Services.DistributionService;
using Server2.Services.HealthCheck;
using Server2.Services.Http;
using Server2.Services.SyncService;
using Server2.Services.Tcp;

namespace Server2.Configurations;
public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());
        
        services.AddSingleton<ISyncService, SyncService>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IDataStorage, DataStorage>();
        services.AddSingleton<IHealthCheckService, HealthCheckService>();
        services.AddSingleton<IDistributionService, DistributionService>();
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<ITcpService, TcpService>();
        services.AddSingleton(typeof(IStorageRepository<>), typeof(StorageRepository<>));
        services.AddHostedService<TcpServerLaunch>();
        services.AddHostedService<DataSync>();
    }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}