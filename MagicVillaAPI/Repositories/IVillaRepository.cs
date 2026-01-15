using MagicVillaAPI.Models;

namespace MagicVillaAPI.Repositories
{
    public interface IVillaRepository
    {
        Task<IEnumerable<Villa>> GetAllAsync();
        Task<Villa?> GetAsync(Guid id);
        Task<Villa> CreateAsync(Villa villa);
        Task<Villa> UpdateAsync(Villa villa);
        Task DeleteAsync(Villa villa);
    }
}