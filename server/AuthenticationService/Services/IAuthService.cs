
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.Common;

namespace ScreenOps.AuthenticationService.Services
{
    public interface IAuthService
    {
        Task<ApiResult<UserSessionDto>> Login(LoginRequestDto req);

        string EncryptPassword(string password);

    }
}
