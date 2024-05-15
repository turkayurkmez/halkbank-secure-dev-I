namespace AuthNZProcess.Api.Services
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class UserService : IUserService
    {
        private List<User> _users = new()
        {
            new User{ Id=1, UserName="turkay", Password="123456", Role="Admin", Email="a@test.com"},
            new User{ Id=2, UserName="kubra", Password="123456", Role="Client", Email="a@test.com"},
            new User{ Id=3, UserName="erkan", Password="123456", Role="Editor", Email="a@test.com"},

        };
        public User? ValidateUser(string userName, string password)
        {
            return _users.SingleOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(password));


        }
    }
}
