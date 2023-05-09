using System.Text.Json.Serialization;

namespace GeoJsonFormatter.Models;

public class Properties
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
