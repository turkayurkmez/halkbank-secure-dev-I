using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNZ.Controllers
{
    [Authorize(Roles ="Admin,Editor")]
    public class IllegalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
