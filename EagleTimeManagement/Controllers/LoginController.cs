using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Questica.ETO.Framework;
using System.Data.SqlClient;
using EagleTimeManagement.Models;

namespace EagleTimeManagement.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyCredentials(UserCredentials creds)
        {
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
    }
}