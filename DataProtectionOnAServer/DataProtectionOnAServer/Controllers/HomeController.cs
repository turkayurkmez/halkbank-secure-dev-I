using DataProtectionOnAServer.Models;
using DataProtectionOnAServer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataProtectionOnAServer.Controllers
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
            string value = "Bu cümle şirelenecek";
            DataProtector dataProtector = new DataProtector("info.txt");
            var encryptedValueLength = dataProtector.EncryptData(value);
            var secretValue = dataProtector.DecryptData(encryptedValueLength);
            ViewBag.Value = secretValue;
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
