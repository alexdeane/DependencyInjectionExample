using System.Globalization;

namespace WeatherForecast.WithoutDependencyInjection;

/// <summary>
/// This class (service) maps the database entity to
/// a DTO for return to the user. This is necessary to
/// decouple the database entity from the return format,
/// as we often don't want to return internal database properties
/// such as primary keys etc.
///
/// Don't worry too much about this, just think of it as some other class that the main
/// program needs to invoke.
/// </summary>
public class WeatherForecastMappingService
{
    /// <summary>
    /// Just maps DB entity to DTO. Might look silly in this
    /// case but is necessary to act as a middle-man to decouple
    /// the database model from the return DTO.
    /// </summary>
    public WeatherForecastDto Map(WeatherForecastEntity entity)
    {
        return new WeatherForecastDto
        {
            Temperature = entity.Temperature,
            Date = entity.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            Summary = entity.Summary
        };
    }
}

/// <summary>
/// A DTO containing only the fields we want to show to the user.
/// Imagine this being serialized to JSON or something and returned
/// from a web API
/// </summary>
public class WeatherForecastDto
{
    public int Temperature { get; init; }
    public string? Date { get; init; }
    public string? Summary { get; init; }
}