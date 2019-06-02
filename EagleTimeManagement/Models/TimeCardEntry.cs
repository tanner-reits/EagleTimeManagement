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
        public decimal hours { get; set; }
        public bool isOnRoad { get; set; }
    }
}
