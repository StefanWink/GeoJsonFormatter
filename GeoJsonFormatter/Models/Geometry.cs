using System.Text.Json.Serialization;

namespace GeoJsonFormatter.Models;

[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(Polygon), "Polygon")]
[JsonDerivedType(typeof(MultiPolygon), "MultiPolygon")]
public class Geometry
{
}
