using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using GeoJsonFormatter.Models;
using NUnit.Framework;

namespace GeoJsonFormatter.Tests.Deserializing;

[TestFixture]
public class MultiPolygonTests
{
    private readonly JsonSerializerOptions options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    [Test]
    public void MultiPolygon()
    {
        // Arrange
        var polygon = new
        {
            type = "MultiPolygon",
            coordinates = new double[][][][]
            {
                new double[][][]
                {
                    new double[][]
                    {
                        new double[] { 30.0, 20.0 },
                        new double[] { 45.0, 40.0 },
                        new double[] { 10.0, 40.0 },
                        new double[] { 30.0, 20.0 }
                    }
                },
                new double[][][]
                {
                    new double[][]
                    {
                        new double[] { 15.0, 5.0 },
                        new double[] { 40.0, 10.0 },
                        new double[] { 10.0, 20.0 },
                        new double[] { 5.0, 10.0 },
                        new double[] { 150, 5.0 }
                    }
                }
            }
        };

        string json = JsonSerializer.Serialize(polygon);

        // Act
        Geometry? geometry = JsonSerializer.Deserialize<Geometry>(json, options);

        // Assert
        geometry.Should().BeOfType<MultiPolygon>();
    }
}
