using AutoMapper;
using ScreenOps.Common;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.AuthenticationService.Models;
using ScreenOps.AuthenticationService.Repositories;
using ScreenOps.AuthenticationService.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            string passwordHash = EncryptionUtils.EncriptPassword(req.Password);

            if (user.PasswordHash != passwordHash)
            {
                return ApiResult<UserSessionDto>.Fail("invalid_credentials");
            }

            DateTime expiresAt = DateTime.UtcNow.AddDays(1);
            string token = EncryptionUtils.GenerateToken(user, expiresAt, _tokenSecret);

            UserSessionDto session = new UserSessionDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            return ApiResult<UserSessionDto>.Ok(session);
        }

    }
}
