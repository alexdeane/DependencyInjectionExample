// This is basically doing the job of what the
// ASP.Net Core framework would be doing for you behind
// the scenes in a web app.

using WeatherForecast.WithoutDependencyInjection;

var appController = new AppController();
await appController.Run();