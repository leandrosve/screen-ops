using ScreenOps.AuthenticationService.Dtos;
using FluentValidation;
using AuthenticationService.Errors;

namespace ScreenOps.AuthenticationService.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(AuthErrors.Login.EmailRequired)
                .EmailAddress().WithMessage(AuthErrors.Login.EmailInvalid)
                .MaximumLength(255).WithMessage(AuthErrors.Login.EmailInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(AuthErrors.Login.PasswordRequired)
                .MinimumLength(6).WithMessage(AuthErrors.Login.PasswordInvalid)
                .MaximumLength(255).WithMessage(AuthErrors.Login.PasswordInvalid);
        }
    }
}
