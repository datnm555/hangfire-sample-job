using System.Text.Json.Serialization;

namespace ScgcJob.Models.Erp;

public class ErpResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("price")] public decimal Price { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("category")] public string Category { get; set; }
    [JsonPropertyName("image")] public string Image { get; set; }
    [JsonPropertyName("rating")] public RatingResponse RatingResponse { get; set; }
}