using System.Text.Json.Serialization;
using GeoJsonFormatter.Converters;

namespace GeoJsonFormatter.Models;

public class MultiPolygon : Geometry
{
    [JsonPropertyName("type")]
    public static GeometryType Type => GeometryType.MultiPolygon;

    [JsonPropertyName("coordinates")]
    [JsonConverter(typeof(MultiPolygonConverter))]
    public double[][][][] Coordinates { get; set; } = null!;
}
