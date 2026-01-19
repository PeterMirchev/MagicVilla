using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.User;

namespace MagicVillaAPI.Services
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(UserCreateRequest request);
        public Task<User> UpdateUserAsync(Guid id, UserUpdateRequest request);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task DisableUserAsync(Guid id);
        Task<User> EnableUserAsync(Guid id);
    }
}
