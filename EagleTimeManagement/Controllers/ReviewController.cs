using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using EagleTimeManagement.Models;

namespace EagleTimeManagement.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            // Create and open connection to SQL database
            SqlConnection conn = new SqlConnection(Startup.connectionString);
            conn.Open();

            // Call stored procedure to get all timecard periods and date ranges
            SqlCommand getPeriods = new SqlCommand("csp0334GetTimecardPeriods", conn) {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            SqlDataReader periodRdr = getPeriods.ExecuteReader();

            // Create list of TimeCardPeriods
            List<TimeCardPeriod> periods = new List<TimeCardPeriod>();
            while(periodRdr.Read())
            {
                periods.Add(new TimeCardPeriod(periodRdr.GetInt32(0), periodRdr.GetString(1)));
            }

            // Close connection to database
            conn.Close();

            // Pass array as model to view
            return View(periods);
        }
    }
}