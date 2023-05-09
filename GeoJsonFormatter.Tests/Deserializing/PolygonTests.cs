using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using GeoJsonFormatter.Models;

namespace GeoJsonFormatter.Tests.Deserializing;

[TestClass]
public class PolygonTests
{
    private readonly JsonSerializerOptions options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    [TestMethod]
    public void Polygon()
    {
        // Arrange
        var polygon = new
        {
            type = "Polygon",
            coordinates = new double[][][]
            {
                new double[][]
                {
                    new double[] { 30.0, 10.0 },
                    new double[] { 40.0, 40.0 },
                    new double[] { 20.0, 40.0 },
                    new double[] { 10.0, 20.0 },
                    new double[] { 30.0, 10.0 }
                }
            }
        };

        string json = JsonSerializer.Serialize(polygon);

        // Act
        Geometry? geometry = JsonSerializer.Deserialize<Geometry>(json, options);

        // Assert
        geometry.Should().BeOfType<Polygon>();
    }

    [TestMethod]
    public void Polygon_with_hole()
    {
        // Arrange
        var polygon = new
        {
            type = "Polygon",
            coordinates = new double[][][]
            {
                new double[][]
                {
                    new double[] { 35.0, 10.0 },
                    new double[] { 45.0, 45.0 },
                    new double[] { 15.0, 40.0 },
                    new double[] { 10.0, 20.0 },
                    new double[] { 35.0, 10.0 }
                },
                new double[][]
                {
                    new double[] { 20.0, 30.0 },
                    new double[] { 35.0, 35.0 },
                    new double[] { 30.0, 20.0 },
                    new double[] { 20.0, 30.0 }
                }
            }
        };

        string json = JsonSerializer.Serialize(polygon);

        // Act
        Geometry? geometry = JsonSerializer.Deserialize<Geometry>(json, options);

        // Assert
        geometry.Should().BeOfType<Polygon>();
    }
}
