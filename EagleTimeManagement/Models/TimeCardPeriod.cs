using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleTimeManagement.Models
{
    public class TimeCardPeriod
    {
        public int ID { get; private set; }
        public string range { get; private set; }

        public TimeCardPeriod(int ID, string range)
        {
            this.ID    = ID;
            this.range = range;
        }
    }
}
