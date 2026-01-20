using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Wallet;

namespace MagicVillaAPI.Services
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(WalletCreateRequest request);
        Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId);
        Task<Wallet> GetWalletByIdAsync(Guid walletId);
        Task<Wallet> UpdateWalletAsync(Guid walletId, WalletUpdateRequest request);
        Task<Wallet> DepositAsync(Guid walletId, decimal amount);
        Task<Wallet> WithdrawAsync(Guid walletId, decimal amount);
    }
}
