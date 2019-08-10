using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Questica.ETO.Framework;
using System.Data.SqlClient;
using EagleTimeManagement.Models;
using EagleTM.Data;

namespace EagleTimeManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuesticaContext context;

        public LoginController(QuesticaContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(UserCredentials creds)
        {
            // Show validation messages if invalid form data
            if(!ModelState.IsValid)
            {
                return View("Index");
            }

            SqlConnection conn = new SqlConnection(Startup.connectionString);
            conn.Open();

            SqlCommand login = new SqlCommand("uspEmployeeLogin", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            login.Parameters.AddWithValue("@nvcUserID", creds.Username);             
            login.Parameters.AddWithValue("@vbcPassword", Questica.ETO.Framework.Helpers.GeneralHelper.EncryptString(creds.Password));
            login.Parameters.AddWithValue("@uidLoginIdentifier", Guid.NewGuid());

            SqlDataReader loginRdr = login.ExecuteReader();

            if (loginRdr.Read())
            {
                Employee emp = new Employee()
                {
                    EmployeeID = loginRdr.GetInt32(loginRdr.GetOrdinal("EmployeeID")),
                    FirstName  = loginRdr.GetString(loginRdr.GetOrdinal("EmpFirstName"))
                };

                HttpContext.Session.SetString("EmpName", emp.FirstName);
                HttpContext.Session.SetInt32("EmpID", emp.EmployeeID);

                conn.Close();
                loginRdr.Close();

                // Route to home page on success
                return Redirect("/Home");
            }

            conn.Close();
            loginRdr.Close();

            // Else route back to Login
            return Redirect("/Login");
        }

        public IActionResult Logout()
        {
            // Clear all session variables
            HttpContext.Session.Clear();

            return Redirect("/Login");
        }
    }
}