using System.Text.Json.Serialization;

namespace ScgcJob.Models.Erp;

public class RatingResponse
{
    [JsonPropertyName("rate")] public decimal Rate { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
}