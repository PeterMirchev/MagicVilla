using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Wallet;
using MagicVillaAPI.Repositories;

namespace MagicVillaAPI.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task<Wallet> CreateWalletAsync(WalletCreateRequest request)
        {
            Wallet wallet = _mapper.Map<Wallet>(request);
            wallet.Balance = 0;
            wallet.CreatedOn = DateTime.UtcNow;
            wallet.UpdatedOn = DateTime.UtcNow;

            return await _walletRepository.CreateAsync(wallet);
        }

        public async Task<Wallet> DepositAsync(Guid walletId, decimal amount)
        {
            ValidateAmount(amount);

            Wallet wallet = await GetWalletByIdAsync(walletId);
            wallet.Balance += amount;
            wallet.UpdatedOn = DateTime.UtcNow;

            return await _walletRepository.UpdateAsync(wallet);
        }

        public async Task<Wallet> GetWalletByIdAsync(Guid walletId)
        {
            return await _walletRepository.GetWalletByIdAsync(walletId)
                ?? throw new KeyNotFoundException($"Wallet with ID '{walletId}' not found.");
        }

        public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId)
        {
            return await _walletRepository.GetWalletsByUserIdAsync(userId);
        }

        public async Task<Wallet> UpdateWalletAsync(Guid walletId, WalletUpdateRequest request)
        {
            Wallet wallet = await GetWalletByIdAsync(walletId);
            wallet.Name = request.Name;
            wallet.UpdatedOn = DateTime.UtcNow;

            return await _walletRepository.UpdateAsync(wallet);
        }

        public async Task<Wallet> WithdrawAsync(Guid walletId, decimal amount)
        {
            Wallet wallet = await GetWalletByIdAsync(walletId);
            ValidateAmount(amount);
            ValidateBalanse(wallet.Balance, amount);

            wallet.Balance -= amount;
            wallet.UpdatedOn = DateTime.UtcNow;

            return await _walletRepository.UpdateAsync(wallet);
        }

        private void ValidateBalanse(decimal balance, decimal amount)
        {
            if (amount > balance)
            {
                throw new InvalidOperationException("Insufficient balance for the withdrawal.");
            }
        }
        private void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
        }
    }
}
