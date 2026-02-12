using MagicVillaAPI.Services.ServiceStack.User.Dto;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Utils
{
    public static class UserMapper
    {

        public static UserStackResponse MapUserToResponse(UserLite user)
        {
            return new UserStackResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOn = DateTime.SpecifyKind(user.CreatedOn, DateTimeKind.Utc),
                UpdatedOn = DateTime.SpecifyKind(user.UpdatedOn, DateTimeKind.Utc),
            };
        }
    }
}
