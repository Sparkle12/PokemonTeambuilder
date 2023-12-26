using Microsoft.IdentityModel.Tokens;
using Server.Models;
using SharedLibrary;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Server.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly Settings _settings;
        private readonly GameDbContext _context;

        public AuthentificationService(Settings settings,GameDbContext context) 
        {
            _settings = settings;
            _context = context;
        }

        public (bool succes, string content) Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username)) return (false, "Invalid username");

            var user = new User() { Username = username, PasswordHash = password };
            user.SaltAndHash();

            _context.Add(user);
            _context.SaveChanges();

            return (true, "");
        }

        public (bool succes, string content) Login(string username,string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user == null) return (false, "Invalid username");

            if (user.PasswordHash != AuthentificationHelpers.ComputeHash(password, user.PasswordSalt))
                return (false, "Invalid password");

            return (true, GenerateJwtToken(AssembleClaimsIdentity(user)));
        }

        private ClaimsIdentity AssembleClaimsIdentity(User user)
        {
            var subject = new ClaimsIdentity(new[] 
            { 
               new Claim("id", user.Id.ToString()) 
            });
            return subject;
        }

        private string GenerateJwtToken(ClaimsIdentity subject)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.BearerKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.Now.AddYears(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };  
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  
        }
    }


    public interface IAuthentificationService
    {
        (bool succes, string content) Register (string username, string password);

        (bool succes, string content) Login(string username, string password);
    }

    public static class AuthentificationHelpers
    { 
        public static void SaltAndHash(this User user)
        {
            var salt = GenerateSalt();
            user.PasswordSalt = Convert.ToBase64String(salt);
            user.PasswordHash = ComputeHash(user.PasswordHash, user.PasswordSalt);
        }

        private static byte[] GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var salt = new byte[24];
            rng.GetBytes(salt);
            return salt;
        }

        public static string ComputeHash(string password,string saltString)
        {
            var salt = Convert.FromBase64String(saltString);

            using var hashGenerator = new Rfc2898DeriveBytes(password,salt);
            hashGenerator.IterationCount = 10101;
            var bytes = hashGenerator.GetBytes(24);

            return Convert.ToBase64String(bytes);
        }
    }

}
