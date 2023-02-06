using System.Text.Json;

namespace WeatherForecast.WithoutDependencyInjection;

/// <summary>
/// Just for reasons, lets say we want to format our
/// output using a separate service. Using a separate class
/// means it can be independently unit tested.
/// </summary>
public class JsonFormatterService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>
    /// This class is initialized with options that affect
    /// how the JSON is serialized
    /// </summary>
    public JsonFormatterService(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    /// <summary>
    /// Just serializes the results to JSON
    /// </summary>
    public async Task<string> SerializeResults(IEnumerable<WeatherForecastDto> results)
    {
        await using var stream = new MemoryStream();

        await JsonSerializer.SerializeAsync(stream, results, _jsonSerializerOptions);

        stream.Position = 0;

        var streamReader = new StreamReader(stream);

        return await streamReader.ReadToEndAsync();
    }
}