using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;
using MagicVillaAPI.Repositories;
using MagicVillaAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace MagicVillaAPI.Test
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepoMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IVillaService> _villServiveMock;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<ReservationService>> _logger;
        private readonly Mock<IWalletService> _walletServiceMock;
        private readonly Mock<IAuditRecordService> _auditServiceMock;

        private readonly ReservationService _reservationService;

        public ReservationServiceTest()
        {
            _reservationRepoMock = new Mock<IReservationRepository>();
            _userServiceMock = new Mock<IUserService>();
            _villServiveMock = new Mock<IVillaService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<ReservationService>>();
            _walletServiceMock = new Mock<IWalletService>();
            _auditServiceMock = new Mock<IAuditRecordService>();

            _reservationService = new ReservationService
                (
                _reservationRepoMock.Object,
                _userServiceMock.Object,
                _villServiveMock.Object,
                _mapper.Object,
                _logger.Object,
                _walletServiceMock.Object,
                _auditServiceMock.Object
                );
        }

        [Fact]
        public async Task CreateReservationAsync_ThenHappyPath()
        {
            var request = new ReservationCreateRequest
            {
                Note = "reservation",
                From = DateOnly.FromDateTime(DateTime.UtcNow),
                To = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)),
                UserId = Guid.NewGuid(),
                VillaId = Guid.NewGuid(),
            };
            var user = new User { Id = request.UserId };
            var villa = new Villa { Id = request.VillaId };
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                Note = request.Note,
                Days = 2,
                From = request.From.ToDateTime(TimeOnly.MinValue),
                To = request.To.ToDateTime(TimeOnly.MinValue),
                User = user,
                Villa = villa,
                UserId = user.Id,
                VillaId = villa.Id
            };
            _userServiceMock
                .Setup(s => s.GetUserByIdAsync(request.UserId)).ReturnsAsync(user);
            _villServiveMock
                .Setup(s => s.GetVillaByIdAsync(request.VillaId)).ReturnsAsync(villa);
            _mapper
                .Setup(m => m.Map<Reservation>(request)).Returns(reservation);
            _reservationRepoMock
                .Setup(r => r.CreateAsync(reservation)).ReturnsAsync(reservation);
            _auditServiceMock
                .Setup(a => a.CreateAsync(
                It.IsAny<Reservation>(), ActionType.RESERVATION_CREATED, user.Id, It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(new AuditRecord());

            var result = await _reservationService.CreateReservationAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.Note, result.Note);

            _reservationRepoMock.Verify(r => r.CreateAsync(It.IsAny<Reservation>()), Times.Once);
            _userServiceMock.Verify(s => s.GetUserByIdAsync(It.IsAny<Guid>()), Times.Once);
            _villServiveMock.Verify(s => s.GetVillaByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mapper.Verify(m => m.Map<Reservation>(request), Times.Once);
            _auditServiceMock.Verify(
                a => a.CreateAsync(It.IsAny<Reservation>(), ActionType.RESERVATION_CREATED, user.Id, It.IsAny<DateTime>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteReservationByIdAsync_ThenHappyPath()
        {
            Guid id = Guid.NewGuid();

            _reservationRepoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            await _reservationService.DeleteReservationByIdAsync(id);

            _reservationRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllRservationsByUserIdAsync_ThenHappyPath()
        {
            Guid userId = Guid.NewGuid();
            var reservations = new List<Reservation>()
            {
                new Reservation {Id = Guid.NewGuid(), UserId = userId},
                new Reservation {Id = Guid.NewGuid(), UserId = userId},
                new Reservation {Id = Guid.NewGuid(), UserId = userId}
             };

            _reservationRepoMock
                .Setup(r => r.GetAllByUserIdAsync(userId)).ReturnsAsync(reservations);

            var result = await _reservationService.GetAllReservationsByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetReservationByIdAsync_ThenHappyPath()
        {
            Guid id = Guid.NewGuid();
            var reservation = new Reservation { Id = id };
            _reservationRepoMock
                .Setup(r => r.GetByIdAsync(id)).ReturnsAsync(reservation);

            var result = await _reservationService.GetReservationByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);

            _reservationRepoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ThenThrowKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _reservationService.GetReservationByIdAsync(It.IsAny<Guid>()));

            _reservationRepoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task PayReservation_ThenHappyPath()
        {
            var request = new ReservationPayRequest { ReservationId = Guid.NewGuid(), UserId = Guid.NewGuid(), WalletId = Guid.NewGuid()};
            var user = new User { Id = request.UserId, Wallets = new List<Wallet>()};
            var wallet = new Wallet { Id = request.WalletId , UserId = request.UserId, User = user};
            user.Wallets.Add(wallet);
            var reservation = new Reservation
            {
                Id = request.ReservationId,
                Days = 2,
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.AddDays(2),
                IsPaid = false,
                Note = "reservation",
                UserId = request.UserId,
                Price = 10
            };
            var paidReservation = new Reservation
            {
                Id = request.ReservationId,
                Days = 2,
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.AddDays(2),
                IsPaid = true,
                Note = "reservation",
                UserId = request.UserId,
                Price = 10,
                DateOfPaimant = DateTime.UtcNow
            };

            _reservationRepoMock.Setup(r => r.GetByIdAsync(request.ReservationId)).ReturnsAsync(reservation);
            _reservationRepoMock.Setup(r => r.UpdateAsync(reservation)).ReturnsAsync(paidReservation);
            _userServiceMock.Setup(u => u.GetUserByIdAsync(request.UserId)).ReturnsAsync(user);
            _walletServiceMock.Setup(w => w.GetWalletByIdAsync(request.WalletId)).ReturnsAsync(wallet);
            _auditServiceMock.Setup(a => a.CreateAsync(reservation, ActionType.RESERVATION_UPDATED, user.Id, It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(new AuditRecord { });

            var result = await _reservationService.PayReservationAsync(request);

            Assert.NotNull(result);
            Assert.True(result.IsPaid);
        }

        [Fact]
        public async Task PayReservation_ThenThrowArgumentException_WhenWalletDoesntBelongToUser()
        {
            var request = new ReservationPayRequest { ReservationId = Guid.NewGuid(), UserId = Guid.NewGuid(), WalletId = Guid.NewGuid() };
            var user = new User { Id = request.UserId, Wallets = new List<Wallet>()};
            var wallet = new Wallet { Id = request.WalletId };
            var reservation = new Reservation
            {
                Id = request.ReservationId,
                IsPaid = false,
                Price = 10
            };
            _reservationRepoMock.Setup(r => r.GetByIdAsync(request.ReservationId)).ReturnsAsync(reservation);
            _userServiceMock.Setup(r => r.GetUserByIdAsync(request.UserId)).ReturnsAsync(user);
            _walletServiceMock.Setup(w => w.GetWalletByIdAsync(request.WalletId)).ReturnsAsync(wallet);

            await Assert.ThrowsAsync<ArgumentException>(() => _reservationService.PayReservationAsync(request));
        }

        [Fact]
        public async Task PayReservation_ThenThrowInvalidOperationException()
        {
            var request = new ReservationPayRequest { ReservationId = Guid.NewGuid(), UserId = Guid.NewGuid(), WalletId = Guid.NewGuid() };
            var reservation = new Reservation
            {
                Id = request.ReservationId,
                IsPaid = true,
                Price = 10
            };
            _reservationRepoMock.Setup(r => r.GetByIdAsync(request.ReservationId)).ReturnsAsync(reservation);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _reservationService.PayReservationAsync(request));
        }
    }
}
