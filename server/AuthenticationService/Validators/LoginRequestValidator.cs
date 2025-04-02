using ScreenOps.AuthenticationService.Dtos;
using FluentValidation;

namespace ScreenOps.AuthenticationService.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email_required")
                .EmailAddress().WithMessage("email_invalid")
                .MaximumLength(255).WithMessage("email_invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password_required")
                .MinimumLength(6).WithMessage("password_invalid")
                .MaximumLength(255).WithMessage("password_invalid");
        }
    }
}
