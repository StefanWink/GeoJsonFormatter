using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using GeoJsonFormatter.Models;
using NUnit.Framework;

namespace GeoJsonFormatter.Tests.Serializing;

[TestFixture]
public class MultiPolygonTests
{
    private readonly JsonSerializerOptions options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter()
        },
        WriteIndented = true
    };

    [Test]
    public void MultiPolygon()
    {
        // Arrange
        MultiPolygon multiPolygon = new()
        {
            Coordinates = new double[][][][]
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
                        new double[] { 15.0, 5.0 }
                    }
                }
            }
        };

        // Act
        string json = JsonSerializer.Serialize<Geometry>(multiPolygon, options);

        // Assert
        json.Should().Be(@"{
  ""type"": ""MultiPolygon"",
  ""coordinates"": [
    [[[30, 20], [45, 40], [10, 40], [30, 20]]],
    [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
  ]
}");
    }

    [Test]
    public void MultiPolygon_with_hole()
    {
        // Arrange
        MultiPolygon multiPolygon = new()
        {
            Coordinates = new double[][][][]
            {
                new double[][][]
                {
                    new double[][]
                    {
                        new double[] { 40.0, 40.0 },
                        new double[] { 20.0, 45.0 },
                        new double[] { 45.0, 30.0 },
                        new double[] { 40.0, 40.0 }
                    }
                },
                new double[][][]
                {
                    new double[][]
                    {
                        new double[] { 20.0, 35.0 },
                        new double[] { 10.0, 30.0 },
                        new double[] { 10.0, 10.0 },
                        new double[] { 30.0, 5.0 },
                        new double[] { 45.0, 20.0 },
                        new double[] { 20.0, 35.0 }
                    },
                    new double[][]
                    {
                        new double[] { 30.0, 20.0 },
                        new double[] { 20.0, 15.0 },
                        new double[] { 20.0, 25.0 },
                        new double[] { 30.0, 20.0 }
                    }
                }
            }
        };

        // Act
        string json = JsonSerializer.Serialize<Geometry>(multiPolygon, options);

        // Assert
        json.Should().Be(@"{
  ""type"": ""MultiPolygon"",
  ""coordinates"": [
    [[[40, 40], [20, 45], [45, 30], [40, 40]]],
    [
      [[20, 35], [10, 30], [10, 10], [30, 5], [45, 20], [20, 35]],
      [[30, 20], [20, 15], [20, 25], [30, 20]]
    ]
  ]
}");
    }
}
