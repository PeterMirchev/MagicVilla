using MagicVillaAPI.Services.ServiceStack.User.Dto;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Repository;

namespace MagicVillaAPI.Services.ServiceStack.User.Services
{
    public class UserServiceLayer
    {
        public IUserLiteRepository _userLiteRepository;

        public UserServiceLayer(UserLiteRepository userLiteRepository)
        {
            _userLiteRepository = userLiteRepository;
        }

        public UserServiceLayer(IUserLiteRepository userLiteRepository)
        {
            _userLiteRepository = userLiteRepository;
        }

        public async Task<UserLite> CreateUser(UserStackRequest request) 
        {

            var user = new UserLite
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _userLiteRepository.CreateAsync(user);

            return await _userLiteRepository.GetByEmailAsync(user.Email);
        }
    }
}
