using AuthNZProcess.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthNZProcess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.ValidateUser(userLogin.UserName, userLogin.Password);
                if (user != null)
                {
                    //Dikkat: bu şifrelenecek olan cümlenin, sabit bir kaynaktan (appsettings.json) okunması elbette daha pratiktir.
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-cümle-kritik-bir-cümledir-aman-önemli"));
                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                        new Claim(ClaimTypes.Role,user.Role)
                    };

                    var token = new JwtSecurityToken(
                        issuer: "api.halkbank",
                        audience: "client.api",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(15),
                        signingCredentials: credential
                        );

                    return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});

                    
                }
                ModelState.AddModelError("login", "Hatalı giriş");
            }
            return BadRequest(ModelState);
        }
    }
}
