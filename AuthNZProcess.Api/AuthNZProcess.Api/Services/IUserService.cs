namespace AuthNZProcess.Api.Services
{
    public interface IUserService
    {
        User? ValidateUser(string userName, string password);
    }
}