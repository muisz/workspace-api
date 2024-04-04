using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private string _key;
        private string _refreshKey;
        private string _issuer;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;

            _key = _configuration["JWT:Key"] ?? "";
            _refreshKey = _key + "44a0d1c6-a78c-499e-bca4-58b8e0023008";
            _issuer = _configuration["JWT:Issuer"] ?? "";
        }

        public TokenData CreateTokenPair(User user)
        {
            string accessToken = CreateAccessToken(user);
            string refreshToken = CreateRefreshToken(user);
            return new TokenData
            {
                Access = accessToken,
                Refresh = refreshToken,
            };
        }

        public string CreateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials,
                issuer: _issuer
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_refreshKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials,
                issuer: _issuer
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? ClaimAccessToken(string token)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_key));
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            
            string email = string.Empty;
            if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = principal.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            }
            return email;
        }

        public string? ClaimRefreshToken(string token)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_refreshKey));
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            
            string email = string.Empty;
            if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = principal.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            }
            return email;
        }
    }
}