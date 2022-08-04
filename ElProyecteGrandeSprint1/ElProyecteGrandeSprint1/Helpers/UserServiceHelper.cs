using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ElProyecteGrandeSprint1.Helpers
{
    public class UserServiceHelper
    {
        private byte[] secret = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));
        public bool ValidateUsername(string userName, DbSet<User> users)
        {
            return Enumerable.All(users, dbUser => dbUser.UserName != userName);
        }

        public string ValidatePassword(RegisterUser user)
        {
            if (user.Password.Length < 5)
            {
                return "The password must be longer than 5 characters";
            }
            else if (user.Password.ToLower().Equals(user.Password))
            {
                return "The password must contain minimum 1 upper letter";
            }
            else
            {
                return "accepted";
            }
        }


        public string JWTGenerator(string email, string userName, long userId)
        {
            var now = DateTime.UtcNow;
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", "customer") }),
                Issuer = "Who issued the token",
                Claims = new Dictionary<string, object>
                {
                    ["userName"] = userName,
                    ["userId"] = userId,
                    ["email"] = email,
                },
                IssuedAt = now,
                NotBefore = now,
                Expires = now + TimeSpan.FromDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);
            return serializedToken;
        }
    }
}
