using System.Text.Json.Serialization;
using GeoJsonFormatter.Converters;

namespace GeoJsonFormatter.Models;

public class Polygon : Geometry
{
    [JsonPropertyName("coordinates")]
    [JsonConverter(typeof(PolygonConverter))]
    public double[][][] Coordinates { get; set; } = null!;
}
