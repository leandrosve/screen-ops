using AutoMapper;
using ScreenOps.Common;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.AuthenticationService.Models;
using ScreenOps.AuthenticationService.Repositories;
using ScreenOps.AuthenticationService.Utils;
using Common.Enums;
using AuthenticationService.Errors;

namespace ScreenOps.AuthenticationService.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UserService(IMapper mapper, IUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApiResult<UserDto>> GetById(Guid userId)
        {
            var res = await _repository.GetById(userId);
            if (res == null) {
                return ApiResult<UserDto>.Fail(UserErrors.Me.UserNotFound);
            }
            var dto = _mapper.Map<UserDto>(res);
            return ApiResult<UserDto>.Ok(dto);
        }

        public async Task<ApiResult<UserDto>> SignUp(SignUpRequestDto request)
        {
            var exists = await _repository.FindByEmail(request.Email);

            if (exists != null)
            {
                return ApiResult<UserDto>.Fail(UserErrors.SignUp.UserAlreadyExists);
            }

            string passwordHash = EncryptionUtils.EncriptPassword(request.Password);
            User user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                Role = UserRole.CLIENT
            };

            await _repository.Insert(user);

            var dto = _mapper.Map<UserDto>(user);

            return ApiResult<UserDto>.Ok(dto);
        }


    }
}
