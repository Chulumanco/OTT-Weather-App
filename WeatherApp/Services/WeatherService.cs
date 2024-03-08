using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Entities;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
      public async Task<WeatherForecast> GetWeatherForecastAsync(string location)
    {
       
        var fakeApiResponse = new WeatherForecast
        {
            Location = location,
            Temperature = 25.5,
            WeatherCondition = "Sunny",
            Humidity = 60
        };

        return fakeApiResponse;
    }
        
    }
}