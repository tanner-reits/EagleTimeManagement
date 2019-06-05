using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EagleTimeManagement.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyCredentials()
        {
            // Route to home page on success
            return Redirect("/Home");

            // Else route back to Login
        }
    }
}