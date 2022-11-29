using Server1.BackgroundTask;
using Server1.Repositories.DataStorage;
using Server1.Repositories.SharedStorage;
using Server1.Services.DataService;
using Server1.Services.DistributionService;
using Server1.Services.HealthCheck;
using Server1.Services.Http;
using Server1.Services.SyncService;
using Server1.Services.Tcp;

namespace Server1.Configurations;
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
        services.AddHostedService<DataSync>();
        services.AddHostedService<TcpServerLaunch>();
        services.AddHostedService<PartitionLeaderHealthCheck>();
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