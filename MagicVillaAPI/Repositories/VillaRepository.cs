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

    public async Task<Wallet> CreateAsync(Wallet wallet)
    {
        _db.Wallets.Add(wallet);
        await  _db.SaveChangesAsync();
        return wallet;
    }

    public Task<Villa> CreateAsync(Villa villa)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Villa villa)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Villa>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Villa> UpdateAsync(Villa villa)
    {
        throw new NotImplementedException();
    }

    Task<Villa?> IVillaRepository.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}