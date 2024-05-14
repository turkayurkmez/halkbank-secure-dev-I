using AuthNZ.Models;

namespace AuthNZ.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>()
        {
            new User{ Id = 1, UserName="turkay", Password="123456", Email="a@b.com", Role="Admin", FullName="Türkay Ü"},
            new User{ Id = 1, UserName="banu", Password="123456", Email="a@b.com", Role="Editor", FullName="Banu Çetin"},
            new User{ Id = 1, UserName="emre", Password="123456", Email="a@b.com", Role="Client", FullName="Emre Özalp"},

        };
        public User? ValidateUser(string username, string password)
        {
            return users.SingleOrDefault(u => u.UserName == username && u.Password == password);

        }
    }
}
