using System.Text.Json;
using System.Xml.Serialization;

namespace WeatherForecast.WithDependencyInjection;

/// <summary>
/// Now it may become more clear why we have a separate service for mapping,
/// and why DI is so nice. I can swap the implementation here for something that
/// formats to XML, HTML, you name it - and I would never have to change any
/// code in the controller or the rest of the application.
/// </summary>
public interface IFormatter
{
    Task<string> SerializeResults(IEnumerable<WeatherForecastDto> results);
}

public class JsonFormatterService : IFormatter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>
    /// This class is initialized with options that affect
    /// how the JSON is serialized.
    ///
    /// The difference now is that this is injected by the service collection,
    /// and not manually by our functional code.
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

        using var streamReader = new StreamReader(stream);

        return await streamReader.ReadToEndAsync();
    }
}

/// <summary>
/// Just for luls, let's say we got a new requirement to format
/// our weather forecasts to XML or something. We can introduce a new
/// implementation of this interface and swap the DI.
/// </summary>
public class XmlFormatterService : IFormatter
{
    /// <summary>
    /// Serialize results to XML
    /// </summary>
    public async Task<string> SerializeResults(IEnumerable<WeatherForecastDto> results)
    {
        var xmlSerializer = new XmlSerializer(typeof(WeatherForecastDto[]));

        await using var stream = new MemoryStream();

        xmlSerializer.Serialize(stream, results.ToArray());

        stream.Position = 0;

        using var streamReader = new StreamReader(stream);

        return await streamReader.ReadToEndAsync();
    }
}