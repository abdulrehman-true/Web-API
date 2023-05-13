using Web_API.Models;

namespace Web_API.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register (User user, string passowrd);
         Task<ServiceResponse<string>> Login(string usernam, string passowrd);
         Task<bool> UserExists(string username);
    }
}