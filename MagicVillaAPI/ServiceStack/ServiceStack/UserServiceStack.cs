using MagicVillaAPI.Services.ServiceStack.User.Dto;
using MagicVillaAPI.Services.ServiceStack.User.Services;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Dto;
using MagicVillaAPI.ServiceStack.ServiceStack.Utils;
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

            var result = UserMapper.MapUserToResponse(user);

            return result;
        }

        public async Task<UserStackResponse> Get(UserStackGetByIdReuqest reuqest)
        {
            var user = await _userServiceLayer.GetUserByIdAsync(reuqest.Id);

            var result = UserMapper.MapUserToResponse(user);

            return result;
        }

        public async Task<UserStackCollectionResponse> Get(UserStackGetAllUsersRequest request)
        {
            var users = await _userServiceLayer.GetAllUsersAsync();

            var result = new UserStackCollectionResponse
            {
                Users = users.Select(u => UserMapper.MapUserToResponse(u)).ToList()
            };

            return result;
        }

        public async Task<UserStackResponse> Get(UserStackGetByEmailRequest request)
        {
            var user = await _userServiceLayer.GetUserByEmailAsync(request.Email);

            var result = UserMapper.MapUserToResponse(user);

            return result;
        }

        public async Task<UserStackResponse> Put(UserStackUpdateRequest request)
        {
            var user = await _userServiceLayer.UpdateUserAsync(request);

            var result = UserMapper.MapUserToResponse(user);

            return result;
        }
    }
}
