using System.Text.Json.Serialization;

namespace GeoJsonFormatter.Models;

public class Feature
{
    [JsonPropertyName("type")]
    public FeatureType Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; } = null!;

    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = null!;
}
