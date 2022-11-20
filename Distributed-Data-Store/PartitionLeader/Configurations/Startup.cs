using PartitionLeader.Repositories.DataStorage;
using PartitionLeader.Repositories.SharedStorage;
using PartitionLeader.Services.DataService;
using PartitionLeader.Services.DistributionService;
using PartitionLeader.Services.Http;
using PartitionLeader.Services.SyncService;

namespace PartitionLeader.Configurations;
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
        services.AddSingleton<IDistributionService, DistributionService>();
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton(typeof(IStorageRepository<>), typeof(StorageRepository<>));
        services.AddHostedService<BackgroundTask.BackgroundTask>();
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