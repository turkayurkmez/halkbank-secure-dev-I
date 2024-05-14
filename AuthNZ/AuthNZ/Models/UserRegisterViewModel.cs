using System.ComponentModel.DataAnnotations;

namespace AuthNZ.Models
{
    public class UserRegisterViewModel
    {
        //YAGNI: You ain't gonna need it!
        //Yak Shaving: Gereksiz işlemleri (ya da özellikleri) traşla

        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      


    }
}
