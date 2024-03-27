
using BlazorLeafletInterop.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherApp;
using WeatherApp.ApiHelper;
using WeatherApp.Services;





var builder = WebAssemblyHostBuilder.CreateDefault(args);
var configuration = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMapService();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

 builder.Services.Configure<ApiConfig>(
    builder.Configuration.GetSection("APIConfig"));
var hostname = configuration["APIConfig:Host"];
Console.WriteLine("Hostname: " + hostname);
builder.Services.AddScoped<WeatherService>();





await builder.Build().RunAsync();
