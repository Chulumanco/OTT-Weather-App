using Moq;
using System.Net;
using System.Net.Http.Json;
using WeatherApp.Data;
using WeatherApp.Entities;
using WeatherApp.Services;
using Xunit;

namespace WeatherApp.UnitTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_Success()
        {
           
            var httpClientMock = new Mock<HttpClient>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x["APIConfig:Host"]).Returns("weatherapi-com.p.rapidapi.com");
            configurationMock.SetupGet(x => x["APIConfig:Key"]).Returns("11f3b87131msha98d8a5c8282a77p10b896jsnc45b7bbd3f42");
            configurationMock.SetupGet(x => x["APIConfig:Url"]).Returns("https://weatherapi-com.p.rapidapi.com/current.json?q=");
            configurationMock.SetupGet(x => x["APIConfig:WeatherApiUrl"]).Returns("https://localhost:7278/api/weather");

            var service = new WeatherService(httpClientMock.Object, configurationMock.Object);

            var result = await service.GetWeatherAsync(37.7749, -122.4194);

            Assert.NotNull(result);
    
        }

        [Fact]
        public async Task SaveWeatherDetailsAsync_Success()
        {
            var httpClientMock = new Mock<HttpClient>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x["APIConfig:WeatherApiUrl"]).Returns("https://localhost:7278/api/weather");

            var service = new WeatherService(httpClientMock.Object, configurationMock.Object);
            var result = await service.SaveWeatherDetailsAsync(new WeatherCorrectionInfo());

            Assert.NotNull(result);
  
        }

        [Fact]
        public async Task GetWeatherHistoryListAsync_Success()
        {

            var httpClientMock = new Mock<HttpClient>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x["APIConfig:WeatherApiUrl"]).Returns("https://localhost:7278/api/weather");

            var service = new WeatherService(httpClientMock.Object, configurationMock.Object);

            var result = await service.GetWeatherHistoryListAsync();
            Assert.NotNull(result);
        }
    }

}
