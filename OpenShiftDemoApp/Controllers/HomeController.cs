using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenShiftDemoApp.Models;

namespace OpenShiftDemoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vars = Environment.GetEnvironmentVariables();
            var envvars = vars.Keys.Cast<string>().ToDictionary(k => k, k => vars[k] as string);
            var model = new HomeIndexViewModel
            {
                EnvVars = envvars
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Session()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
