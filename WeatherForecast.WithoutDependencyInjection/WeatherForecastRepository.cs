namespace WeatherForecast.WithoutDependencyInjection;

/// <summary>
/// Imagine this is actually a service which
/// interacts with a database and returns data.
/// </summary>
public class WeatherForecastRepository
{
    /// <summary>
    /// For brevity and the purposes of example,
    /// this just returns hardcoded data. 
    /// </summary>
    public IEnumerable<WeatherForecastEntity> GetForecasts()
    {
        return new WeatherForecastEntity[]
        {
            new()
            {
                Temperature = 32,
                Date = new DateOnly(
                    year: 2023,
                    month: 2,
                    day: 5
                ),
                Summary = "Partly cloudy",

                // These are like database-specific meta properties which I only added
                // to make the mapping from entity to DTO seem more necessary.
                Id = Guid.NewGuid(),
                CreatedDate = new DateTime(2023, 05, 02, 5, 2, 1),
                UpdatedDate = new DateTime(2023, 05, 02, 5, 2, 1),
            },
            new()
            {
                Temperature = 0,
                Date = new DateOnly(
                    year: 2023,
                    month: 2,
                    day: 4
                ),
                Summary = "Sunny",
                Id = Guid.NewGuid(),
                CreatedDate = new DateTime(2023, 05, 02, 5, 2, 1),
                UpdatedDate = new DateTime(2023, 05, 02, 5, 2, 1),
            }
        };
    }
}

/// <summary>
/// Pretend this is a database entity POCO,
/// which is used by EFCore or something and aligns
/// with a database table.
/// </summary>
public class WeatherForecastEntity : Entity
{
    public int Temperature { get; init; }
    public DateOnly Date { get; init; }
    public string? Summary { get; init; }
}

/// <summary>
/// Don't worry too much about this inheritance.
/// I just want to cleanly separate the pretend
/// database-specific fields for clarity
/// </summary>
public abstract class Entity
{
    public Guid Id { get; init; }

    public DateTime CreatedDate { get; init; }

    public DateTime UpdatedDate { get; init; }
}