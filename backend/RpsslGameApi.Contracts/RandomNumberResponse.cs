using System.Text.Json.Serialization;

namespace RpsslGameApi.Contracts;

public class RandomNumberResponse
{
    [JsonPropertyName("random_number")]
    public required int RandomNumber { get; set; }
}