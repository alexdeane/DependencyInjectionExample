using System.Text.Json;

namespace WeatherForecast.WithoutDependencyInjection;

/// <summary>
/// You can pretend that this is an API controller or something
/// </summary>
public class AppController
{
    /// <summary>
    /// Runs the program
    /// </summary>
    public Task Run()
    {
        var repository = new WeatherForecastRepository();
        var mapper = new WeatherForecastMappingService();
        var outputService = new JsonFormatterService(new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var weatherForecasts = repository.GetForecasts();
        var results = weatherForecasts.Select(mapper.Map);

        return outputService.SerializeResults(results)
            .ContinueWith(x =>
            {
                Console.WriteLine(x.Result);
            });
    }
}