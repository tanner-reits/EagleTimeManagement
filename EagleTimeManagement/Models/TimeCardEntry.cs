using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EagleTimeManagement.Models
{
    public class TimeCardEntry
    {
        [Display(Name = "Date")]
        public string date { get; set; }

        [Display(Name = "Project")]
        public string project { get; set; }

        [Display(Name = "Station")]
        public string station { get; set; }

        [Display(Name = "Labor Code")]
        public string laborCode { get; set; }

        [Display(Name = "Hours")]
        public decimal hours { get; set; }

        public bool isOnRoad { get; set; }
    }
}
