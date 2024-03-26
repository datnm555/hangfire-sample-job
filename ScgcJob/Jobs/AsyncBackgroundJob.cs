using Hangfire;
using Hangfire.Server;
using ScgcJob.Jobs.ErpJob;
using ScgcJob.Services;

namespace ScgcJob.Jobs;

public  class AsyncBackgroundJob : BackgroundService
{
    private readonly IRecurringJobManager _recurringJobs;
    private readonly ILogger<RecurringJobScheduler> _logger;

    public AsyncBackgroundJob(IRecurringJobManager recurringJobs, ILogger<RecurringJobScheduler> logger)
    {
        _recurringJobs = recurringJobs;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _recurringJobs.AddOrUpdate<ErpRetrieveDatJob>("ErpRetrieveData",
                x => x.RetrieveDataFromErpAsync(), Cron.Hourly, new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while creating recurring jobs.");
        }
    }
}