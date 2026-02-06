using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Wallet;
using MagicVillaAPI.Repositories;
using MagicVillaAPI.Services;
using Moq;

namespace MagicVillaAPI.Test
{
    public class WalletServiceTest
    {

        private readonly Mock<IWalletRepository> _walletRepoMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly WalletService _walletService;

        public WalletServiceTest()
        {
            _walletRepoMock = new Mock<IWalletRepository>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();

            _walletService = new WalletService(
                _walletRepoMock.Object,
                _userServiceMock.Object,
                _mapperMock.Object
                );
        }

        [Fact]
        public async Task CreateWalletAsync_ThenHappyPath()
        {
            Guid userId = Guid.NewGuid();
            var request = new WalletCreateRequest
            {
                Name = "wallet",
                Currency = "EUR",
                UserId = userId
            };
            var wallet = new Wallet()
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            var user = new User()
            {
                Id = userId,
                Email = "test@test.com",
            };

            _userServiceMock
                .Setup(s => s.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _mapperMock
                .Setup(m => m.Map<Wallet>(It.IsAny<WalletCreateRequest>()))
                .Returns(wallet);
            _walletRepoMock
                .Setup(r => r.CreateAsync(wallet))
                .ReturnsAsync(wallet);

            var result = await _walletService.CreateWalletAsync(request);

            Assert.NotNull(result);
            Assert.Equal(wallet.Id, result.Id);

            _walletRepoMock.Verify(r => r.CreateAsync(It.IsAny<Wallet>()), Times.Once);
            _userServiceMock.Verify(s => s.GetUserByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DepositAsync_ThenHappyPath()
        {
            Guid walletId = Guid.NewGuid();
            decimal amount = 10;

            var wallet = new Wallet()
            {
                Id = walletId,
                Name = "wallet",
                Balance = 0
            };
            var walletAfterDeposit = new Wallet()
            {
                Id = walletId,
                Name = "wallet",
                Balance = amount
            };

            _walletRepoMock
                .Setup(r => r.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);
            _walletRepoMock
                .Setup(r => r.UpdateAsync(wallet))
                .ReturnsAsync(walletAfterDeposit);

            var result = await _walletService.DepositAsync(walletId, amount);

            Assert.NotNull(result);
            Assert.Equal(amount, result.Balance);

            _walletRepoMock.Verify(r => r.UpdateAsync(It.Is<Wallet>(w => w.Balance == amount)), Times.Once);
        }

        [Fact]
        public async Task DepositeAsync_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _walletService.DepositAsync(It.IsAny<Guid>(), -1));
        }

        [Fact]
        public async Task GetWalletById_ThenHappyPath()
        {
            Guid walletId = Guid.NewGuid();
            var wallet = new Wallet()
            {
                Id = walletId,
                Name = "wallet"
            };
            _walletRepoMock
                .Setup(r => r.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);

            var result = await _walletService.GetWalletByIdAsync(walletId);

            Assert.NotNull(result);
            Assert.Equal(wallet.Id, result.Id);

            _walletRepoMock.Verify(r => r.GetWalletByIdAsync(walletId), Times.Once);
        }

        [Fact]
        public async Task GetWalletById_ThenThrowKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _walletService.GetWalletByIdAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetWalletsByUserId_ThenHappyPath()
        {
            var userId = Guid.NewGuid();
            var wallets = new List<Wallet>()
            {
                new Wallet
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                },
                new Wallet
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                }
            };
            _walletRepoMock
                .Setup(r => r.GetWalletsByUserIdAsync(userId))
                .ReturnsAsync(wallets);

            var result = await _walletService.GetWalletsByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            _walletRepoMock.Verify(r => r.GetWalletsByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task UpdateWalletAsync_ThenHappyPath()
        {
            Guid walletId = Guid.NewGuid();
            var request = new WalletUpdateRequest { Name = "new name" };
            var wallet = new Wallet { Id = walletId, Name = "old name" };

            _walletRepoMock
                .Setup(r => r.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);
            _walletRepoMock
                .Setup(r => r.UpdateAsync(wallet))
                .ReturnsAsync(new Wallet { Id = walletId, Name = request.Name });

            var result = await _walletService.UpdateWalletAsync(walletId, request);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);

            _walletRepoMock.Verify(r => r.UpdateAsync(wallet), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_ThenHappyPath()
        {
            Guid walletId = Guid.NewGuid();
            decimal amount = 10;

            var wallet = new Wallet { Id = walletId, Name = "wallet", Balance = 10 };
            var walletAfterWithdraw = new Wallet { Id = walletId, Name = "wallet", Balance = 0 };

            _walletRepoMock
                .Setup(r => r.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);
            _walletRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Wallet>()))
                .ReturnsAsync(walletAfterWithdraw);

            var result = await _walletService.WithdrawAsync(walletId, amount);

            Assert.NotNull(result);
            Assert.Equal(0, result.Balance);
        }

        [Fact]
        public async Task WithdrawAsync_ThenThrowInvalidOperationException()
        {
            Guid walletId = Guid.NewGuid();
            decimal amount = 20;
            var wallet = new Wallet { Id = walletId, Name = "wallet", Balance = 10 };

            _walletRepoMock
                .Setup(r => r.GetWalletByIdAsync(walletId))
                .ReturnsAsync(wallet);

            await Assert.ThrowsAsync<InvalidOperationException>
                            (() => _walletService.WithdrawAsync(walletId, amount));
        }
    }
}
