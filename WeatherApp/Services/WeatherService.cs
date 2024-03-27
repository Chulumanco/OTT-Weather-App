
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text;
using WeatherApp.ApiHelper;
using WeatherApp.Data;
using WeatherApp.Entities;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace WeatherApp.Services
{
    public class WeatherService
    {

        private readonly HttpClient _httpClient;
       

        private readonly IConfiguration _configuration;


        public WeatherService( HttpClient httpClient, IConfiguration configuration)
        {
   
            _httpClient = httpClient;
            _configuration = configuration;
            
        }


        public async Task<RootobjectModel> GetWeatherAsync(double latitude, double longitude)
        {
           
            string apiUrl = $"{GetApiConfig().Url}{Convert.ToDecimal(latitude)}%2C{Convert.ToDecimal(longitude)}";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("X-RapidAPI-Key", GetApiConfig().Key);
            request.Headers.Add("X-RapidAPI-Host", GetApiConfig().Host);

            try
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await System.Text.Json.JsonSerializer.DeserializeAsync<RootobjectModel>(responseStream);

                        return data;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
        }


       
        public async Task<(string ResponseContent, HttpStatusCode StatusCode)> SaveWeatherDetailsAsync(WeatherCorrectionInfo data)
        {
            try
            {
                string fullUrl = $"{GetApiConfig().WeatherApiUrl}/saveweather";
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(fullUrl, data);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                return (responseContent, response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApiException($"An error occurred: {ex.Message}");
            }
        }
        public async Task<List<WeatherCorrectionInfo>> GetWeatherHistoryListAsync()
        {
            try
            {
                string fullUrl = $"{GetApiConfig().WeatherApiUrl}/history";

                var response = await _httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<List<WeatherCorrectionInfo>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                throw new ApiException($"Failed to retrieve data. Status code: {response.StatusCode}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw new ApiException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new ApiException($"An error occurred: {ex.Message}");
            }
        }
        private ApiConfig GetApiConfig()
        {
            try
            { 
                return new ApiConfig
                {
                    Host = _configuration["APIConfig:Host"],
                    Key = _configuration["APIConfig:Key"],
                    Url = _configuration["APIConfig:Url"],
                    WeatherApiUrl = _configuration["APIConfig:WeatherApiUrl"],
                };
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("An error occurred while fetching API configuration:");
                Console.WriteLine(ex.Message);
                return null; // or throw the exception if appropriate
            }
        }


    }



}
