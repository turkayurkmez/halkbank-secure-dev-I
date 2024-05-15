using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthNZProcess.Api.Security
{
    public class BasicHandler : AuthenticationHandler<BasicOptions>
    {
        public BasicHandler(IOptionsMonitor<BasicOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            //  $.ajax({
            //url: 'https://localhost/Products',
            //    headers:
            //    {
            //        'Authorization':'Basic ' + btoa('turkay:123')
            //    }
            //});

            /*
             * 1. Gelen istek, 'Authorization' barındırıyor mu?
             * 2. 'Authorization' değeri standarda uygun mu?
             * 3. 'Authorization' şeması Basic mi?
             * 4. Şema, değer için uygun mu? Uygunsa decode et.
             * 5. ':' işaretine göre ayır. İlki kullanıcı adı ikincisi şifredir.
             */
            //1.
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2.
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue? parsedValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //3.

            if (!parsedValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            //4.
            var bytes = Convert.FromBase64String(parsedValue.Parameter);
            var headerValue = Encoding.UTF8.GetString(bytes);

            //5:
            var userName = headerValue.Split(':')[0];
            var pass= headerValue.Split(":")[1];

            if (userName == "turkay" && pass == "123")
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName)

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity (claims);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal (claimsIdentity);

                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            return Task.FromResult(AuthenticateResult.Fail("Hatalı giriş"));
        }
    }
}
