namespace WeatherApp.ApiHelper
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}
