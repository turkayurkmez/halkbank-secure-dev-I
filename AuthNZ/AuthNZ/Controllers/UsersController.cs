using AuthNZ.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthNZ.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(UserRegisterViewModel user) 
        {
            //bu action'dan sadece "müşteri" rolüne dahil olunabilir.
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }
    }
}
