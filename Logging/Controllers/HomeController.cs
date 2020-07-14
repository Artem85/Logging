using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logging.Models;

namespace Logging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string logMsg = $"Index page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(logMsg);

            return View();
        }

        public IActionResult Privacy()
        {
            string logMsg = $"Privacy page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogWarning(logMsg);
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
