namespace ScreenOps.AuthenticationService.Validators
{
    using ScreenOps.AuthenticationService.Dtos;
    using FluentValidation;

    public class SignUpRequestDtoValidator : AbstractValidator<SignUpRequestDto>
    {
        public SignUpRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("first_name_required")
             .MaximumLength(100).WithMessage("first_name_too_long");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("last_name_required")
                .MaximumLength(100).WithMessage("last_name_too_long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email_required")
                .EmailAddress().WithMessage("email_invalid")
                .MaximumLength(255).WithMessage("email_too_long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password_required")
                .MinimumLength(6).WithMessage("password_too_short")
                .MaximumLength(255).WithMessage("password_too_long");
        }
    }
}
