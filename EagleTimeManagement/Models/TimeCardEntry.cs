using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleTimeManagement.Models
{
    public class TimeCardEntry
    {
        public string date { get; set; }
        public string project { get; set; }
        public string station { get; set; }
        public string laborCode { get; set; }
        public int hours { get; set; }
        public bool isOnRoad { get; set; }

        public TimeCardEntry(string date, string project, string station, string laborCode, int hours, bool onRoad)
        {
            this.date = date;
            this.project = project;
            this.station = station;
            this.laborCode = laborCode;
            this.hours = hours;
            this.isOnRoad = onRoad;
        }
    }
}
