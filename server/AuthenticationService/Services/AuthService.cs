using AutoMapper;
using ScreenOps.Common;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.AuthenticationService.Models;
using ScreenOps.AuthenticationService.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace ScreenOps.AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly string _tokenSecret;

        public AuthService(IMapper mapper, IUserRepository repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _tokenSecret = configuration.GetValue<string>("JwtSettings:Key") ?? throw new ArgumentNullException("missing_token_sign_key");
        }

        public async Task<ApiResult<UserSessionDto>> Login(LoginRequestDto req)
        {
            User? user = await _repository.FindByEmail(req.Email);

            if (user == null)
            {
                return ApiResult<UserSessionDto>.Fail("invalid_credentials");
            }

            string passwordHash = EncryptPassword(req.Password);

            if (user.PasswordHash != passwordHash)
            {
                return ApiResult<UserSessionDto>.Fail("invalid_credentials");
            }

            DateTime expiresAt = DateTime.UtcNow.AddDays(1);
            string token = GenerateToken(user, expiresAt, _tokenSecret);

            UserSessionDto session = new UserSessionDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            return ApiResult<UserSessionDto>.Ok(session);
        }

        private string GenerateToken(User user, DateTime expiresAt, string tokenSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));

            string role = user.Role.ToString(); // Dont know why it warns if I do this on Claim constructor

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
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

        public string EncryptPassword(string rawPassword)
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

    }
}
