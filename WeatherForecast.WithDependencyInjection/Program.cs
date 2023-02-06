using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.WithDependencyInjection;

// First we create a service collection, which is a special
// Microsoft class for managing dependency injection and service lifetimes.
// This thing comes from Microsoft.Extensions.DependencyInjection.
// In ASP.Net Core, you wouldn't have to create one of these yourself -
// The ConfigureServices method in Program.cs would already have one for you 
// to use.
var serviceCollection = new ServiceCollection();

// Next, we add EVERY service that we intend to inject into another service into
// the service collection. There are different methods for this which affect the lifecycle
// of the service you add, but I'm just using AddSingleton for simplicity. You can google
// about service lifetimes if you want to learn more about what that means.
serviceCollection.AddSingleton<AppController>();

// // You can switch this line for the one below to switch between
// // JSON and XML output.
// serviceCollection.AddSingleton<IFormatter, JsonFormatterService>();
serviceCollection.AddSingleton<IFormatter, XmlFormatterService>();
serviceCollection.AddSingleton<IWeatherForecastMappingService, WeatherForecastMappingService>();
serviceCollection.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
serviceCollection.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web));

// Now, we just pull out our controller and run it. The service collection automatically
// injects the other services into its constructor when we pull it out.
// In real ASP.Net Core, you wouldn't ever really need to pull one of these out yourself - the framework
// does it for you, and automatically pulls your controllers out when a request comes in.

// Don't worry about this line. Just allows us to pull out services.
var serviceProvider = serviceCollection.BuildServiceProvider();
var controller = serviceProvider.GetRequiredService<AppController>();

await controller.Run();