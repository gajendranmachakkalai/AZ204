using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace EmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppSettings _appSettings;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IOptions<MyAppSettings> appSettings,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _env = env;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Current environment: {_env.EnvironmentName}");
            string msg = $"App: {_appSettings.AppName}, Version: {_appSettings.Version}";
            ViewBag.Message = msg;
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
