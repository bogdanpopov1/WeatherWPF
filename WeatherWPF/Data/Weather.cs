using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWPF.Data
{
    public class Weather
    {
        public Weather(DateTime date, int temp, WeatherStatus status, int windSpeed, double rainAmount)
        {
            Id = Guid.NewGuid().ToString();
            Date = date;
            Temp = temp;
            Status = status;
            WindSpeed = windSpeed;
            RainAmount = rainAmount;
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int Temp { get; set; }
        public WeatherStatus Status { get; set; }
        public int WindSpeed { get; set; }
        public double RainAmount { get; set; }
    }
}
