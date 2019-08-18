using EagleTM.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EagleTimeManagement.Models
{
    public class EditViewModel
    {
        public TblTimecards TimeCard { get; set; }

        [DisplayFormat(DataFormatString = @"{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public IEnumerable<SelectListItem> DateRange { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public List<SelectListItem> Stations { get; set; }

        public List<SelectListItem> LaborCodes { get; set; }

        public EditViewModel()
        {
            this.Projects = new List<SelectListItem>();
            this.Stations = new List<SelectListItem>();
            this.LaborCodes = new List<SelectListItem>();
        }


        public IEnumerable<SelectListItem> PopulateDatesInPayPeriod(DateTime PayPeriodStartDate, DateTime PayPeriodEndDate)
        {
            return Enumerable.Range(0, 1 + PayPeriodEndDate
                .Subtract(PayPeriodStartDate).Days)
                .Select(offset =>
                new SelectListItem
                {
                    Value = PayPeriodStartDate.AddDays(offset).ToString(),
                    Text = PayPeriodStartDate.AddDays(offset).ToString("MM/dd/yyyy")
                }).ToList();
        }
    }
}
