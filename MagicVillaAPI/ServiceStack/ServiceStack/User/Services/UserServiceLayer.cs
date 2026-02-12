using MagicVillaAPI.Services.ServiceStack.User.Dto;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Dto;
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
                Role = Role.USER,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

           return await _userLiteRepository.CreateAsync(user);
        }

        public async Task<UserLite> GetUserByIdAsync(Guid id)
        {
            return await _userLiteRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"User with ID '{id}' not found.");
        }

        public async Task<UserLite> UpdateUserAsync(UserStackUpdateRequest request)
        {
            var user = await GetUserByIdAsync(request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.UpdatedOn = DateTime.UtcNow;
           
            var result = await _userLiteRepository.UpdateAsync(user);
            return result;
        }

        public async Task<List<UserLite>> GetAllUsersAsync()
        {
            return await _userLiteRepository.GetAllAsync();
        }

        public async Task<UserLite> GetUserByEmailAsync(string email)
        {
            return await _userLiteRepository.GetByEmailAsync(email)
                ?? throw new KeyNotFoundException($"User with email '{email}' not found.");
        }
    }
}
