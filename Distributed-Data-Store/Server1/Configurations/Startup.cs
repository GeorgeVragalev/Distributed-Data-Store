using Server1.Repositories.DataStorage;
using Server1.Repositories.SharedStorage;
using Server1.Services.DataService;
using Server1.Services.DistributionService;
using Server1.Services.SyncService;

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
        services.AddSingleton<IDistributionService, DistributionService>();
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