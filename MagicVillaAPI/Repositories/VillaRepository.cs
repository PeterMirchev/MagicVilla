using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositories;

public class VillaRepository : IWalletRepository
{
    private readonly AppDbContext _db;

    public VillaRepository(AppDbContext db)
    { 
        _db = db;
    }
    
  
}