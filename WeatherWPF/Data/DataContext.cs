using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWPF.Data
{
    public class DataContext
    {
        public static List<WeatherStatus> WeatherStatuses = new List<WeatherStatus>() 
        { 
            new WeatherStatus("Ясно"),
            new WeatherStatus("Малооблачно"),
            new WeatherStatus("Облачно"),
            new WeatherStatus("Пасмурно"),
        };

        public static List<Weather> Weathers = new List<Weather>()
        {
            new Weather(new DateTime(2024, 05, 10), 10, WeatherStatuses[0], 5, 0),
            new Weather(new DateTime(2024, 05, 11), 20, WeatherStatuses[1], 15, 0.1),
            new Weather(new DateTime(2024, 05, 12), 4, WeatherStatuses[2], 2, 0.2),
            new Weather(new DateTime(2024, 05, 13), 7, WeatherStatuses[3], 7, 0.5),
            new Weather(new DateTime(2024, 05, 14), 20, WeatherStatuses[0], 17, 0),
            new Weather(new DateTime(2024, 05, 15), 6, WeatherStatuses[2], 20, 0.2),
            new Weather(new DateTime(2024, 05, 16), -10, WeatherStatuses[2], 4, 0.6),
            new Weather(new DateTime(2024, 05, 17), -12, WeatherStatuses[3], 6, 1),
            new Weather(new DateTime(2024, 05, 18), -12, WeatherStatuses[3], 0, 1.5),
            new Weather(new DateTime(2024, 05, 19), 4, WeatherStatuses[2], 3, 0),
        };
    }
}
