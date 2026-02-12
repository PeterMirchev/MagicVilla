using FluentValidation;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Dto;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Utils
{
    public class UsersStackGetByEmailValidator : AbstractValidator<UserStackGetByEmailRequest>
    {
        public UsersStackGetByEmailValidator()
        {
           RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}
