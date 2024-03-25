using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ScgcJob.Services;

namespace ScgcJob.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    public JobsController(IJobService jobService, IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManager)
    {
        _jobService = jobService;
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
    }

    [HttpGet("/get-erp-data")]
    public async Task<ActionResult> GetDataFromErp()
    {
        // await _jobService.GetDataFromErp();
        
        // _recurringJobManager.AddOrUpdate<ErpStoreDatJobArgs>("Erp storing data",
        //     job => job.ExecuteAsync(new ErpStoreDatJobArgs()), Cron.Monthly(3), new RecurringJobOptions
        //     {
        //         MisfireHandling = MisfireHandlingMode.Relaxed
        //     });
        //
        // _backgroundJobClient.Enqueue(() => _jobService.GetDataFromErp());
        return Ok();
    }
}