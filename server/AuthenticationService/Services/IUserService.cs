
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.Common;

namespace ScreenOps.AuthenticationService.Services
{
    public interface IUserService
    {
        Task<ApiResult<UserDto>> SignUp(SignUpRequestDto request);

        Task<ApiResult<UserDto>> GetById(Guid userId);
    }
}
