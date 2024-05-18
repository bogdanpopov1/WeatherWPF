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
            new Weather(new DateTime(2024, 05, 1), 10, WeatherStatuses[0], 5, 0),
            new Weather(new DateTime(2024, 05, 2), 20, WeatherStatuses[1], 10, 0.5),
            new Weather(new DateTime(2024, 05, 3), 10, WeatherStatuses[3], 5, 0.3),
            new Weather(new DateTime(2024, 05, 4), 20, WeatherStatuses[2], 15, 0.1),
            new Weather(new DateTime(2024, 05, 5), 10, WeatherStatuses[1], 8, 0.9),
            new Weather(new DateTime(2024, 05, 6), 20, WeatherStatuses[1], 12, 0.2),
            new Weather(new DateTime(2024, 05, 7), 10, WeatherStatuses[0], 5, 0),
            new Weather(new DateTime(2024, 05, 8), 20, WeatherStatuses[2], 14, 0.1),
            new Weather(new DateTime(2024, 05, 9), 10, WeatherStatuses[3], 3, 0),
            new Weather(new DateTime(2024, 05, 10), 20, WeatherStatuses[0], 15, 0.1),
            new Weather(new DateTime(2024, 05, 11), 20, WeatherStatuses[3], 19, 0.2),
            new Weather(new DateTime(2024, 05, 12), 10, WeatherStatuses[2], 5, 0),
            new Weather(new DateTime(2024, 05, 13), 20, WeatherStatuses[2], 12, 0.5),
            new Weather(new DateTime(2024, 05, 14), 10, WeatherStatuses[1], 7, 1),
            new Weather(new DateTime(2024, 05, 15), 20, WeatherStatuses[1], 15, 0.1),
            new Weather(new DateTime(2024, 05, 16), 10, WeatherStatuses[0], 5, 0),
            new Weather(new DateTime(2024, 05, 17), 20, WeatherStatuses[1], 10, 0.1),
            new Weather(new DateTime(2024, 05, 18), 10, WeatherStatuses[0], 5, 0),
            new Weather(new DateTime(2024, 05, 19), 20, WeatherStatuses[1], 15, 0.1),
            new Weather(new DateTime(2024, 05, 20), 4, WeatherStatuses[2], 2, 0.2),
            new Weather(new DateTime(2024, 05, 21), 7, WeatherStatuses[3], 7, 0.5),
            new Weather(new DateTime(2024, 05, 22), 20, WeatherStatuses[0], 17, 0),
            new Weather(new DateTime(2024, 05, 23), 6, WeatherStatuses[2], 20, 0.2),
            new Weather(new DateTime(2024, 05, 24), -10, WeatherStatuses[2], 4, 0.6),
            new Weather(new DateTime(2024, 05, 25), -12, WeatherStatuses[3], 6, 1),
            new Weather(new DateTime(2024, 05, 26), -12, WeatherStatuses[3], 0, 1.5),
            new Weather(new DateTime(2024, 05, 27), 4, WeatherStatuses[2], 3, 0),
            new Weather(new DateTime(2024, 05, 28), -12, WeatherStatuses[3], 6, 1),
            new Weather(new DateTime(2024, 05, 29), -12, WeatherStatuses[3], 0, 1.5),
        };
    }
}
