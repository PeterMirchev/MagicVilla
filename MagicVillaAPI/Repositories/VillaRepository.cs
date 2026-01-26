using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositories;

public class VillaRepository : IVillaRepository
{
    private readonly AppDbContext _db;

    public VillaRepository(AppDbContext db)
    { 
        _db = db;
    }

    public async Task<Villa> CreateAsync(Villa villa)
    {
        _db.Villas.Add(villa);
        await _db.SaveChangesAsync();
        return villa;
    }

    public async Task DeleteAsync(Villa villa)
    {
        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Villa>> GetAllAsync()
    {
        return await _db.Villas.ToListAsync();
    }

    public async Task<Villa> UpdateAsync(Villa villa)
    {
        _db.Villas.Update(villa);
        await _db.SaveChangesAsync();
        return villa;
    }

    async Task<Villa?> IVillaRepository.GetByIdAsync(Guid id)
    {
        return await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
    }
}