using Hangfire;
using Hangfire.SqlServer;
using Refit;
using ScgcJob.Services;
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

        services.AddHangfire(x =>
            x.UseSqlServerStorage(configuration.GetConnectionString("defaultConnection")));
        JobStorage.Current = new SqlServerStorage(configuration.GetConnectionString("defaultConnection"));
        services.AddHangfireServer();

        BackgroundJobConfigurator();
    }

    private static void BackgroundJobConfigurator()
    {
        RecurringJob.RemoveIfExists("ErpRetrieveData");
        RecurringJob.AddOrUpdate<IJobService>("ErpRetrieveData",
            x => x.GetDataFromErp(), Cron.Hourly);
    }
}