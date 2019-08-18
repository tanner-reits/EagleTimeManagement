using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using EagleTimeManagement.Models;
using Microsoft.AspNetCore.Http;
using EagleTM.Data;

namespace EagleTimeManagement.Controllers
{
    public class ReviewController : Controller
    {
        private readonly QuesticaContext context;

        public ReviewController(QuesticaContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //Redirect user to login screen if not currently logged in
            if (HttpContext.Session.GetInt32("EmpID") == null)
            {
                return Redirect("/login");
            }

            ViewData["EmpName"] = HttpContext.Session.GetString("EmpName");

            // Create and open connection to SQL database
            SqlConnection conn = new SqlConnection(Startup.connectionString);
            conn.Open();

            // Call stored procedure to get all timecard periods and date ranges
            SqlCommand getPeriods = new SqlCommand("csp0334GetTimecardPeriods", conn);
            SqlDataReader periodRdr = getPeriods.ExecuteReader();

            // Create list of TimeCardPeriods
            List<TimeCardPeriod> periods = new List<TimeCardPeriod>();
            while(periodRdr.Read())
            {
                periods.Add(new TimeCardPeriod(periodRdr.GetInt32(0), periodRdr.GetString(1)));
            }

            // Close connection to database
            conn.Close();
            periodRdr.Close();

            // Pass array as model to view
            return View(periods);
        }

        public IActionResult loadTable (int id)
        {
            SqlConnection conn = new SqlConnection(Startup.connectionString);
            conn.Open();

            SqlCommand getEntries = new SqlCommand("csp0334GetTimecardEntries", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            getEntries.Parameters.AddWithValue("@EmployeeID", HttpContext.Session.GetInt32("EmpID"));
            getEntries.Parameters.AddWithValue("@TimePeriodID", id);

            var data = getEntries.ExecuteReader();

            var entries = new List<TimeCardEntry>();
            while (data.Read())
            {
                entries.Add(new TimeCardEntry()
                {
                    date = data.GetString(data.GetOrdinal("Date")),
                    project = data.GetString(data.GetOrdinal("Project")),
                    station = data.GetString(data.GetOrdinal("Station")),
                    laborCode = data.GetString(data.GetOrdinal("LaborCode")),
                    hours = data.GetDecimal(data.GetOrdinal("Hours"))
                });
            }

            conn.Close();
            data.Close();

            return PartialView(entries);
        }
    }
}