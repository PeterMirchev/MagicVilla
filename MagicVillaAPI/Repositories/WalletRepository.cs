using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _db;

        public WalletRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Wallet> CreateAsync(Wallet wallet)
        {
            _db.Wallets.Add(wallet);
            await _db.SaveChangesAsync();
            return wallet;
        }

        public async Task<Wallet?> GetWalletByIdAsync(Guid walletId)
        {
            return await _db.Wallets.FindAsync(walletId);
        }

        public async Task<Wallet?> GetWalletByNameAsync(string walletName)
        {
            return await _db.Wallets.FirstOrDefaultAsync(w => w.Name.ToLower() == walletName.ToLower());
        }

        public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId)
        {
            return await _db.Wallets.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<Wallet> UpdateAsync(Wallet wallet)
        {
            _db.Wallets.Update(wallet);
            await _db.SaveChangesAsync();
            return wallet;
        }
    }
}
