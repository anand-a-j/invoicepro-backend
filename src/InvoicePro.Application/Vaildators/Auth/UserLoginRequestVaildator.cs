using FluentValidation;
using InvoicePro.Application.DTOs.Auth;

namespace InvoicePro.Application.Validators.Auth;

public class UserLoginRequestValidator
    : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}