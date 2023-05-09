using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJsonFormatter.Converters;

public class PolygonConverter : JsonConverter<double[][][]>
{
    public override double[][][]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (double[][][]?)JsonSerializer.Deserialize(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, double[][][] value, JsonSerializerOptions options)
    {
        int depth = writer.CurrentDepth;
        double[][][] polygon = value;

        StringBuilder sb = new("[");

        if (polygon.Length > 1)
        {
            sb.AppendLine();
            depth++;
        }

        for (int i = 0; i < polygon.Length; i++)
        {
            if (polygon.Length > 1)
            {
                sb.Append(' ', depth * 2);
            }

            double[][] ring = polygon[i];
            WriteRing(sb, ring);

            if (i < polygon.Length - 1)
            {
                sb.Append(',');
                sb.AppendLine();
            }
        }

        if (polygon.Length > 1)
        {
            depth--;
            sb.AppendLine();
            sb.Append(' ', depth * 2);
        }

        sb.Append(']');

        writer.WriteRawValue(sb.ToString());
    }

    private static void WriteRing(StringBuilder sb, double[][] ring)
    {
        sb.Append('[');

        for (int i = 0; i < ring.Length; i++)
        {
            double[] coordinate = ring[i];

            sb.Append($"[{Round(coordinate[0])}, {Round(coordinate[1])}]");

            if (i < ring.Length - 1)
            {
                sb.Append(", ");
            }
        }

        sb.Append(']');
    }

    private static string Round(double value)
    {
        return Math.Round(value, 4, MidpointRounding.AwayFromZero).ToString();
    }
}
