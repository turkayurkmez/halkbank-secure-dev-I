using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNZ.Controllers
{
    [Authorize]
    public class IllegalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
