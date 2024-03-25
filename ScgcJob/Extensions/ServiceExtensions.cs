using Hangfire;
using Hangfire.SqlServer;
using Refit;
using ScgcJob.Jobs;
using ScgcJob.Services.Integrations;

namespace ScgcJob.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureErp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IErpApi>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri(configuration["ErpApi:Url"] ?? string.Empty); });
    }

    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRecurringJobManager, RecurringJobManager>();
        services.AddSingleton<IBackgroundJobClient, BackgroundJobClient>();
        services.AddHostedService<AsyncBackgroundJob>();
       

        services.AddHangfire(x =>
            x.UseSqlServerStorage(configuration.GetConnectionString("defaultConnection")));
        JobStorage.Current = new SqlServerStorage(configuration.GetConnectionString("defaultConnection"));
        services.AddHangfireServer(options =>
        {
            options.StopTimeout = TimeSpan.FromSeconds(15);
            options.ShutdownTimeout = TimeSpan.FromSeconds(30);
        });

        // BackgroundJobConfigurator();
    }

    // private static void BackgroundJobConfigurator()
    // {
    //     RecurringJob.RemoveIfExists("ErpRetrieveData");
    //     RecurringJob.AddOrUpdate<ErpRetrieveDataJob>("ErpRetrieveData",
    //         x => x.ExecuteJobAsync(new ErpRetrieveDataJobArgs()), Cron.Hourly);
    // }
}