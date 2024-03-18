
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

namespace WeatherApp.Services
{
    public class WeatherService
    {

        private readonly HttpClient _httpClient;
       

        // private readonly ApiConfig _apiSettings;


        public WeatherService( HttpClient httpClient, IOptions<ApiConfig> apiSettings)
        {
   
            _httpClient = httpClient;
            // _apiSettings = apiSettings.Value;
        }


        public async Task<RootobjectModel> GetWeatherAsync(double latitude, double longitude)
        {
            string apiUrl = $"https://weatherapi-com.p.rapidapi.com/current.json?q={Convert.ToDecimal(latitude)}%2C{Convert.ToDecimal(longitude)}";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("X-RapidAPI-Key", "11f3b87131msha98d8a5c8282a77p10b896jsnc45b7bbd3f42");
            request.Headers.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

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

        //public async Task SaveWeatherDetailsAsync(WeatherCorrectionInfo weatherDetails)
        //{
        //    try
        //    {
        //        weatherDetails.RecordedAt = DateTime.Now;
        //        _context.WeatherCorrectionInfos.Add(weatherDetails);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred while saving weather details: {ex.Message}");
        //        throw; 
        //    }
        //}
       
        public async Task<(string ResponseContent, HttpStatusCode StatusCode)> SaveWeatherDetailsAsync(WeatherCorrectionInfo data)
        {
            try
            {
                string fullUrl = "https://localhost:7278/api/weather/saveweather";
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
                string fullUrl = "https://localhost:7278/api/weather/history";

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


    }



}
