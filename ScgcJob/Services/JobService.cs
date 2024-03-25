using System.Net;
using Hangfire.Server;
using Polly;
using Refit;
using ScgcJob.Models.Erp;
using ScgcJob.Services.Integrations;

namespace ScgcJob.Services;

public class JobService : IJobService
{
    private readonly IErpApi _erpApi;

    public JobService(IErpApi erpApi)
    {
        _erpApi = erpApi;
    }

    public async Task GetDataFromErp()
    {
        var response = await Policy<ApiResponse<IEnumerable<ErpResponse>>>
            .HandleResult(x => x.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(3, duration => TimeSpan.FromMicroseconds(30))
            .ExecuteAsync(async () => await _erpApi.GetErpDataAsync());

        if (response.StatusCode != HttpStatusCode.OK || response.Content is null)
        {
            throw new Exception();
        }

        Console.WriteLine($"Archiving file: test");
    }
}