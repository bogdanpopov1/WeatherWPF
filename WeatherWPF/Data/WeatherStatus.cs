using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWPF.Data
{
    public class WeatherStatus
    {
        public WeatherStatus(string name)
        {
            Id = Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }

    }
}
