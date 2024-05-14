using AuthNZ.Models;
using AuthNZ.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthNZ.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
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
        public ActionResult RegisterUser(UserRegisterViewModel user) 
        {
            //least privilege: En az ayrıcalık ilkesi, her rolün sadece yapabildiklerine odaklanır. Varsayılan olarak diğer her şey yasaktır.

            //buna göre müşteri rolü oluşturulur.
            //bu action'dan sadece "müşteri" rolüne dahil olunabilir.
            
            if (ModelState.IsValid)
            {
                //Şifresini, müşteriden başka kimse bilemez
              
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                ViewBag.Hash = passwordHash;
               
                return View("Success");
            }

            return View();
        }

        public IActionResult Login(string? gidilecekSayfa)
        {
            var model = new UserLoginViewModel();
            model.ReturnUrl = gidilecekSayfa;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user, string? gidilecekSayfa)
        {
            if (ModelState.IsValid)
            {
                //Kullanıcı giriş yaptığında onaylama başka bir sınıf tarafından üstlenilmeli
                //bu değişken, veritabanında bulunan kullanıcıya ait hash:

                var passwordHash = "";
                //Not: Bu işlem, artık user service instance'inin görevi....
                BCrypt.Net.BCrypt.Verify(user.Password, passwordHash);

                var validatedUser = userService.ValidateUser(user.UserName, user.Password);
                if (validatedUser != null)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name,validatedUser.FullName),
                        new Claim(validatedUser.Email,validatedUser.Email),
                        new Claim(ClaimTypes.Role, validatedUser.Role)

                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (!string.IsNullOrEmpty(gidilecekSayfa) && Url.IsLocalUrl(gidilecekSayfa))
                    {
                        return Redirect(gidilecekSayfa);
                    }
                    return Redirect("/");

                }              
            }


            ModelState.AddModelError("login", "Hatalı kullanıcı girişi!");
            return View();
                

        }
    }
}
