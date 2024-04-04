using WorkspaceAPI.Data;
using WorkspaceAPI.Models;

namespace WorkspaceAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher _hasher;

        public AuthService(DatabaseContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public User Register(AuthRegisterData payload)
        {
            User? userWithSameUsername = GetUserByUsername(payload.Username);
            if (userWithSameUsername != null)
                throw new Exception("Username already exist.");
            
            User? userWithSameEmail = GetUserByEmail(payload.Email);
            if (userWithSameEmail != null)
                throw new Exception("Email already exist.");
            
            string hash = _hasher.Hash(payload.Password);
            User user = new User
            {
                Name = payload.Name,
                Username = payload.Username.ToLower(),
                Email = payload.Email.ToLower(),
                Password = hash,
                EmailVerified = false,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetUserByUsername(string value)
        {
            return _context.Users.SingleOrDefault(user => user.Username.Equals(value));
        }

        public User? GetUserByEmail(string value)
        {
            return _context.Users.SingleOrDefault(user => user.Email.Equals(value));
        }

        public User Authenticate(string email, string password)
        {
            User? user = GetUserByEmail(email);
            if (user == null)
                throw new Exception("Email not found.");
            
            if (!_hasher.Check(password, user.Password))
                throw new Exception("Wrong password.");
            
            return user;
        }

        public void VerifyEmail(User user)
        {
            user.EmailVerified = true;
            _context.SaveChanges();
        }
    }
}
