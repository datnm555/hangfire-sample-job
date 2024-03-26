using System.Net;
using Polly;
using Refit;
using ScgcJob.Models.Erp;
using ScgcJob.Services.Integrations;

namespace ScgcJob.Jobs.ErpJob;

public class ErpRetrieveDatJob
{
    private readonly IErpApi _erpApi;

    public ErpRetrieveDatJob(IErpApi erpApi)
    {
        _erpApi = erpApi;
    }

    public async Task RetrieveDataFromErpAsync()
    {
        var response = await Policy<ApiResponse<IEnumerable<ErpResponse>>>
            .HandleResult(x => x.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(3, duration => TimeSpan.FromMicroseconds(30))
            .ExecuteAsync(async () => await _erpApi.GetErpDataAsync());

        if (response.StatusCode != HttpStatusCode.OK || response.Content is null)
        {
            throw new Exception();
        }

        Console.WriteLine($"Storing to FB");

        foreach (var item in response.Content)
        {
            Console.WriteLine(
                $"{item.Id} {item.Title} {item.Price} {item.Description} {item.Category} {item.Image}");
        }
    }
}