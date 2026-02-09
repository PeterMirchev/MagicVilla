using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Repository
{
    public interface IUserLiteRepository
    {
        Task<UserLite> GetByIdAsync(Guid id);
        Task<UserLite> GetByEmailAsync(string email);
        Task<List<UserLite>> GetAllAsync();
        Task<UserLite> CreateAsync(UserLite user);
        Task<UserLite> UpdateAsync(UserLite user);
        Task<bool> DeleteAsync(Guid id);
    }
}
