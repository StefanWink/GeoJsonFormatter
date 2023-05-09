using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJsonFormatter.Models;

namespace GeoJsonFormatter.ConsoleApp;

internal class Program
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide one argument.");
            Console.WriteLine("Usage: GeoJsonFormatter.ConsoleApp [input-file-path]");
            return 1;
        }

        string input = args[0];
        FileInfo inputFile = new(input);

        if (!inputFile.Exists)
        {
            Console.WriteLine($"input-file-path: {inputFile.FullName}");
            Console.WriteLine("File not found.");
            return 1;
        }

        JsonDocumentOptions options = new()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip
        };

        using (FileStream inputStream = File.OpenRead(input))
        {
            JsonDocument doc = JsonDocument.Parse(inputStream, options);

            JsonSerializerOptions serializerOptions = new()
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true
            };

            FeatureCollection? geoJson = doc.Deserialize<FeatureCollection>(serializerOptions);

            JsonSerializer.Serialize(Console.OpenStandardOutput(), geoJson, serializerOptions);
        }

        return 0;
    }
}
