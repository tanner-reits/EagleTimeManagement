using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EagleTimeManagement.Models;
using EagleTM.Data;

namespace EagleTimeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuesticaContext context;

        public HomeController(QuesticaContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            // Redirect user to login screen if not currently logged in

            if (HttpContext.Session.GetInt32("EmpID") == null)
            {
                return Redirect("/Login");
            }

            ViewData["EmpName"] = HttpContext.Session.GetString("EmpName");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
