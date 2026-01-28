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
        public ReservationService(IReservationRepository reservationRepository,
            IUserService userService,
            IVillaService villaService,
            IMapper mapper,
            ILogger<ReservationService> logger)
        {
            _reservationRepository = reservationRepository;
            _userService = userService;
            _villaService = villaService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationCreateRequest request)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            var villa = await _villaService.GetVillaByIdAsync(request.VillaId);

            Reservation reservation = _mapper.Map<Reservation>(request);
            reservation.CreatedOn = DateTime.UtcNow;
            reservation.UpdatedOn = DateTime.UtcNow;

            CalculateReservationDays(reservation);
            reservation.Price = villa.PricePerDay * reservation.Days;

            var persistedReservation = await _reservationRepository.CreateAsync(reservation);

            _logger.LogInformation("Reservation created. ReservationId={ReservationId}, UserId={UserId}",
                persistedReservation.Id, persistedReservation.UserId);

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
