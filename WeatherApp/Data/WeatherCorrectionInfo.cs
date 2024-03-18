using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Data
{
    public class WeatherCorrectionInfo
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Current location is required.")]
        public string CurrentLocation { get; set; }
        [Required(ErrorMessage = "The Temperature of location is required.")]
        public double Temperature { get; set; }
        [Required(ErrorMessage = "The WeatherCondition of location is required.")]
        public string WeatherCondition { get; set; }

        [Range(0, 100, ErrorMessage = "Humidity must be between 0 and 100.")]
        public int Humidity { get; set; }

        [Range(0, 100, ErrorMessage = "Clouds must be between 0 and 100.")]
        public int Clouds { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
