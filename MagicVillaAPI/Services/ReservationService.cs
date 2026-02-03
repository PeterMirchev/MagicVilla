using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;
using MagicVillaAPI.Repositories;

namespace MagicVillaAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserService _userService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationService> _logger;
        private readonly IWalletService _walletService;
        private readonly IAuditRecordService _auditRecordService;

        public ReservationService(IReservationRepository reservationRepository,
            IUserService userService,
            IVillaService villaService,
            IMapper mapper,
            ILogger<ReservationService> logger,
            IWalletService walletService,
            IAuditRecordService auditRecordService)
        {
            _reservationRepository = reservationRepository;
            _userService = userService;
            _villaService = villaService;
            _mapper = mapper;
            _logger = logger;
            _walletService = walletService;
            _auditRecordService = auditRecordService;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationCreateRequest request)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            var villa = await _villaService.GetVillaByIdAsync(request.VillaId);

            Reservation reservation = _mapper.Map<Reservation>(request);
            reservation.IsPaid = false;
            reservation.CreatedOn = DateTime.UtcNow;
            reservation.UpdatedOn = DateTime.UtcNow;

            CalculateReservationDays(reservation);
            reservation.Price = villa.PricePerDay * reservation.Days;

            var persistedReservation = await _reservationRepository.CreateAsync(reservation);

            _logger.LogInformation("Reservation created. ReservationId={ReservationId}, UserId={UserId}",
                persistedReservation.Id, persistedReservation.UserId);

            string details = $"User with ID '{user.Id}' successfully created a reservation with ID '{reservation.Id}'.";
            await _auditRecordService.CreateAsync(reservation, ActionType.RESERVATION_CREATED, user.Id, DateTime.UtcNow, details);

            return persistedReservation;
        }

        public async Task DeleteReservationByIdAsync(Guid reservationId)
        {
            await _reservationRepository.DeleteAsync(reservationId);
            _logger.LogInformation($"Reservation with ID '{reservationId}' successfully deleted.");
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsByUserIdAsync(Guid userId)
        {
            return await _reservationRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid reservationId)
        {
            return await _reservationRepository.GetByIdAsync(reservationId)
                ?? throw new KeyNotFoundException($"Reservation with ID '{reservationId}' not found.");
        }

        public Task<Reservation> UpdateReservationAsync(ReservationUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> PayReservationAsync(ReservationPayRequest request)
        {
            var reservation = await GetReservationByIdAsync(request.ReservationId);
            if (reservation.IsPaid)
            {
                throw new InvalidOperationException("Reservation already paid");
            }

            var user = await _userService.GetUserByIdAsync(request.UserId);
            await ValidateMetadata(user, request.WalletId);

            await _walletService.WithdrawAsync(request.WalletId, reservation.Price);
            reservation.IsPaid = true;
            reservation.DateOfPaimant = DateTime.UtcNow;

            _logger.LogInformation("Reservation successfully paid. ReservationId={ReservationId}", reservation.Id);
            string details = "Reservatopn successfully paid.";
            await _auditRecordService.CreateAsync(reservation, ActionType.RESERVATION_UPDATED, user.Id, DateTime.UtcNow, details);

            return await _reservationRepository.UpdateAsync(reservation);
        }

        private async Task ValidateMetadata(User user, Guid walletId)
        {
            var wallet = await _walletService.GetWalletByIdAsync(walletId);
            if (!user.Wallets.Any(w => w.Id == walletId))
            {
                throw new ArgumentException($"Wallet {walletId} does not belong to user {user.Id}");
            }
        }

        private void CalculateReservationDays(Reservation reservation)
        {
            if (reservation.To <= reservation.From)
            {
                throw new ArgumentException("End date must be after start date");
            }
            reservation.Days = (reservation.To.Date - reservation.From.Date).Days;
        }
    }
}
