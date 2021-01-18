using System;

namespace Demo
{
    //public class WeatherForecast
    //{
    //    public WeatherForecast()
    //    {
    //        Date = DateTime.Now;
    //    }

    //    public DateTime Date { get; init; }

    //    public int TemperatureC { get; set; }

    //    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    //    public string Summary { get; set; }
    //}

    public record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

}
