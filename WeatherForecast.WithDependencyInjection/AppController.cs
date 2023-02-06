using System.Text.Json;

namespace WeatherForecast.WithDependencyInjection;

/// <summary>
/// You can pretend that this is an API controller or something
/// </summary>
public class AppController
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IWeatherForecastMappingService _mapper;
    private readonly IFormatter _formatter;

    /// <summary>
    /// The class accepts its dependencies in the constructor. You would
    /// say that the class has its dependencies "injected" at runtime,
    /// rather than having to instantiate them itself.
    ///
    /// This allows the code of this class to be VERY decoupled from the rest of the code,
    /// making unit testing much easier and making the code quite a bit more modular. 
    /// </summary>
    public AppController(
        IWeatherForecastRepository repository,
        IWeatherForecastMappingService mapper,
        IFormatter formatter
    )
    {
        _repository = repository;
        _mapper = mapper;
        _formatter = formatter;
    }

    /// <summary>
    /// Runs the program. Does basically the same thing as
    /// the version without DI, but doesn't need to instantiate anything.
    /// </summary>
    public Task Run()
    {
        var weatherForecasts = _repository.GetForecasts();
        var results = weatherForecasts.Select(_mapper.Map);

        return _formatter.SerializeResults(results.ToArray())
            .ContinueWith(x =>
            {
                Console.WriteLine(x.Result);
            });
    }
}