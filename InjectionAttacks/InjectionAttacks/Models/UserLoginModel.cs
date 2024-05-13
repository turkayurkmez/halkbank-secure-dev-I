using System.ComponentModel.DataAnnotations;


namespace InjectionAttacks.Models
{
    public class UserLoginModel
    {

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]       
        [DataType(DataType.MultilineText)]
        public string UserInfo { get; set; }

    }
}
