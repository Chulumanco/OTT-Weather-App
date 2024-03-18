
using BlazorLeafletInterop.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using WeatherApp;
using WeatherApp.ApiHelper;
using WeatherApp.Data;
using WeatherApp.Services;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
var configuration = builder.Configuration;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMapService();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Revisit,  APIConfig coming back as null
//builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("APIConfig"));

builder.Services.AddScoped<WeatherService>();





await builder.Build().RunAsync();
