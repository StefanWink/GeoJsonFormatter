using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using GeoJsonFormatter.Models;

namespace GeoJsonFormatter.Tests.Serializing;

[TestClass]
public class PolygonTests
{
    private readonly JsonSerializerOptions options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        },
        WriteIndented = true
    };

    [TestMethod]
    public void Polygon()
    {
        // Arrange
        Polygon polygon = new()
        {
            Coordinates = new double[][][]
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

        // Act
        string json = JsonSerializer.Serialize<Geometry>(polygon, options);

        // Assert
        json.Should().Be(@"{
  ""type"": ""Polygon"",
  ""coordinates"": [[[30, 10], [40, 40], [20, 40], [10, 20], [30, 10]]]
}");
    }

    [TestMethod]
    public void Polygon_with_hole()
    {
        // Arrange
        Polygon polygon = new()
        {
            Coordinates = new double[][][]
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

        // Act
        string json = JsonSerializer.Serialize<Geometry>(polygon, options);

        // Assert
        json.Should().Be(@"{
  ""type"": ""Polygon"",
  ""coordinates"": [
    [[35, 10], [45, 45], [15, 40], [10, 20], [35, 10]],
    [[20, 30], [35, 35], [30, 20], [20, 30]]
  ]
}");
    }
}
