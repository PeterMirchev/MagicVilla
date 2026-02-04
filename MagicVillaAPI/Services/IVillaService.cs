using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Villa;

namespace MagicVillaAPI.Services;

public interface IVillaService
{
    public Task<Villa> GetVillaByIdAsync(Guid id);
    
    public Task<List<Villa>> GetAllVillasAsync();
    
    public Task<Villa> CreateVillaAsync(VillaCreateRequest request);
    
    public Task<Villa> UpdateVillaAsync(VillaUpdateRequest request, Guid id);
    
    public Task DeleteVillaAsync(Guid id);
}