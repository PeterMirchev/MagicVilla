using MagicVillaAPI.Models;

namespace MagicVillaAPI.Repositories
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId);
        Task<Wallet?> GetWalletByIdAsync(Guid walletId);
        Task<Wallet?> GetWalletByNameAsync(string walletName);
        Task<Wallet> CreateAsync(Wallet wallet);
        Task<Wallet> UpdateAsync(Wallet wallet);
    }
}
