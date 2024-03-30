using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public interface IJWTService
    {
        public Token CreateTokenPair(User user);
        public string CreateAccessToken(User user);
        public string CreateRefreshToken(User user);
        public string? ClaimAccessToken(string token);
        public string? ClaimRefreshToken(string token);
    }
}
