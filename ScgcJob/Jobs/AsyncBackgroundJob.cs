using Hangfire;
using Hangfire.Server;
using ScgcJob.Services;

namespace ScgcJob.Jobs;

public abstract class AsyncBackgroundJob : BackgroundService
{
    private readonly IRecurringJobManager _recurringJobs;
    private readonly ILogger<RecurringJobScheduler> _logger;

    protected AsyncBackgroundJob(IRecurringJobManager recurringJobs, ILogger<RecurringJobScheduler> logger)
    {
        _recurringJobs = recurringJobs;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _recurringJobs.AddOrUpdate<IJobService>("ErpRetrieveData",
                x => x.GetDataFromErp(), Cron.Hourly, new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time")
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred while creating recurring jobs.");
        }
    }
}