using ScreenOps.Common;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using Common.Enums;

namespace ScreenOps.AuthenticationService.Controllers
{

    [Tags("Auth")]
    [Route("auth")]
    public class AuthController : BaseAuthController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService) {
            _authService = authService; 
            _userService = userService;
        }

        [HttpPost("login", Name = "Log In")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserSessionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            ApiResult<UserSessionDto> res = await _authService.Login(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPost("signup", Name = "Sign Up")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(SignUpRequestDto dto)
        {
            ApiResult<UserDto> res = await _userService.SignUp(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);


        }

        [HttpGet("me", Name = "Me")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Me()
        {
            Guid userId = GetUserId();
            ApiResult<UserDto> res = await _userService.GetById(userId);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
