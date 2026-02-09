using MagicVillaAPI.Services.ServiceStack.User.Dto;
using MagicVillaAPI.Services.ServiceStack.User.Services;
using ServiceStack;

namespace MagicVillaAPI.Services.ServiceStack
{
    public class UserServiceStack : Service
    {
        private readonly UserServiceLayer _userServiceLayer;

        public UserServiceStack(UserServiceLayer userServiceLayer)
        {
            _userServiceLayer = userServiceLayer;
        }

        public async Task<UserStackResponse> Post(UserStackRequest request)
        {

            var user = await _userServiceLayer.CreateUser(request);

            var result = new UserStackResponse
            {
               Id = user.Id,
               FirstName = user.FirstName,
               LastName = user.LastName,
               PhoneNumber = user.PhoneNumber,
               Email = user.Email,
               IsActive = user.IsActive,
               CreatedOn = user.CreatedOn,
               UpdatedOn = user.UpdatedOn,
            };
            return result;
        }
    }
}
