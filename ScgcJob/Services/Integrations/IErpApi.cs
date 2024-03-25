using Refit;
using ScgcJob.Models.Erp;

namespace ScgcJob.Services.Integrations;

public interface IErpApi
{
    [Get("/products")]
    Task<ApiResponse<IEnumerable<ErpResponse>>> GetErpDataAsync();
}