using FUNewsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FUNewsManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly ILogger<HomeController> _logger;

        // =================================
        // === Constructors
        // =================================

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // =================================
        // === Actions
        // =================================

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
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
