using ScreenOps.AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScreenOps.AuthenticationService.Utils
{
    public class EncryptionUtils
    {

        public static string EncriptPassword(string rawPassword)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(rawPassword));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static string GenerateToken(User user, DateTime expiresAt, string tokenSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString()),
            };
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
