using System.Text.Json.Serialization;

namespace GeoJsonFormatter.Models;

public class FeatureCollection
{
    [JsonPropertyName("type")]
    public FeatureType Type { get; set; }

    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = new();
}
