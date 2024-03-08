using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Entities
{
    public class WeatherForecast
    {
      public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Location { get; set; }
    public double Temperature { get; set; }
    public string WeatherCondition { get; set; }
    public int Humidity { get; set; }
    }
}