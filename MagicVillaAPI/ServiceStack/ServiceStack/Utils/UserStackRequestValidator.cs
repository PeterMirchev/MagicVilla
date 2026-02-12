using FluentValidation;
using MagicVillaAPI.Services.ServiceStack.User.Dto;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Utils
{
    public class UserStackRequestValidator : AbstractValidator<UserStackRequest>
    {

        public UserStackRequestValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}
