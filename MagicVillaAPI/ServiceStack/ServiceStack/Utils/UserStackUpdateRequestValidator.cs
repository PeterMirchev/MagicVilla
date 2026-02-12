using FluentValidation;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Dto;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Utils
{
    public class UserStackUpdateRequestValidator : AbstractValidator<UserStackUpdateRequest>
    {
        public UserStackUpdateRequestValidator() 
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("User ID is required.");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}
