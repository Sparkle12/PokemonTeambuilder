using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Repository;
using SharedLibrary;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Settings _settings;
        private readonly GameDbContext _context;
        private readonly RoleRepository _roleRep;
        private readonly UserRepository _userRep;
        private readonly int userRoleId;

        public AuthenticationService(Settings settings,GameDbContext context,RoleRepository roleRep,UserRepository userRep) 
        {
            _settings = settings;
            _context = context;
            _roleRep = roleRep;
            _userRep = userRep;
            userRoleId = 3; //default role when registering
        }

        public  (bool succes, string content) Register(string username, string password)
        {
            //if (_context.Users.Any(u => u.Username == username)) return (false, "Invalid username");


            var user = new User() { Username = username, PasswordHash = password ,Role = _roleRep.FindById(userRoleId)};
            user.SaltAndHash();

            _context.Add(user);
            _context.SaveChanges();

            return (true, "");
        }

        public (bool succes, string content) Login(string username,string password)
        {
            var user = _userRep.FindByUsername(username);

            if (user == null) return (false, "Invalid username");

            if (user.PasswordHash != AuthenticationHelpers.ComputeHash(password, user.PasswordSalt))
                return (false, "Invalid password");

            return (true, GenerateJwtToken(AssembleClaimsIdentity(user)));
        }

        private ClaimsIdentity AssembleClaimsIdentity(User user)
        {
            var subject = new ClaimsIdentity(new[] 
            { 
               new Claim("id", user.Id.ToString()),
               new Claim("role",user.Role.Id.ToString())
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


    public interface IAuthenticationService
    {
        (bool succes, string content) Register (string username, string password);

         (bool succes, string content) Login(string username, string password);
    }

    public static class AuthenticationHelpers
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
