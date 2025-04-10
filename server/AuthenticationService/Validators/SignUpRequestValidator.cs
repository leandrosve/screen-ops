using ScreenOps.AuthenticationService.Dtos;
using FluentValidation;
using AuthenticationService.Errors;

namespace ScreenOps.AuthenticationService.Validators
{
    public class SignUpRequestDtoValidator : AbstractValidator<SignUpRequestDto>
    {
        public SignUpRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage(UserErrors.SignUp.FirstNameRequired)
             .MaximumLength(100).WithMessage(UserErrors.SignUp.FirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(UserErrors.SignUp.LastNameRequired)
                .MaximumLength(100).WithMessage(UserErrors.SignUp.LastNameMaxLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(UserErrors.SignUp.EmailRequired)
                .EmailAddress().WithMessage(UserErrors.SignUp.EmailInvalid)
                .MaximumLength(255).WithMessage(UserErrors.SignUp.EmailMaxLength);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(UserErrors.SignUp.PasswordRequired)
                .MinimumLength(6).WithMessage(UserErrors.SignUp.PasswordMinLength)
                .MaximumLength(255).WithMessage(UserErrors.SignUp.PasswordMaxLength);
        }
    }
}
