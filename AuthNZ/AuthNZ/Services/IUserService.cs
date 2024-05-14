using AuthNZ.Models;

namespace AuthNZ.Services
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}
