using InjectionAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Encodings.Web;


namespace InjectionAttacks.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JavaScriptEncoder javaScriptEncoder;
        private readonly HtmlEncoder htmlEncoder;

        public HomeController(ILogger<HomeController> logger, JavaScriptEncoder javaScriptEncoder, HtmlEncoder htmlEncoder)
        {
            _logger = logger;
            this.javaScriptEncoder = javaScriptEncoder;
            this.htmlEncoder = htmlEncoder;
        }

        public IActionResult Index()
        {
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel userLogin)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True");

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE UserName=@name AND Password = @password", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", userLogin.UserName);
            sqlCommand.Parameters.AddWithValue("@password", userLogin.Password);

            sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                ViewBag.IsLoggedIn = true;
                return View("Index");
            }

            ViewBag.IsLoggedIn = false;
            return View();


        }

       
        public IActionResult CreateUser()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                //userLoginModel.UserInfo = htmlEncoder.Encode(userLoginModel.UserInfo);
                userLoginModel.UserInfo = javaScriptEncoder.Encode(userLoginModel.UserInfo);
                return View(userLoginModel);
            }

            return View();
            
        
        }
    }
}
