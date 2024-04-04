using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IAuthService
    {
        public User Register(AuthRegisterData payload);
        public User? GetUserByUsername(string value);
        public User? GetUserByEmail(string value);
        public User Authenticate(string email, string password);
        public void VerifyEmail(User user);
    }
}
