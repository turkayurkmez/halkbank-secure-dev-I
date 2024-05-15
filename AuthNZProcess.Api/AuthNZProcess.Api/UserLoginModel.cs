using System.ComponentModel.DataAnnotations;

namespace AuthNZProcess.Api
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]      
        public string Password { get; set; }

    }
}
