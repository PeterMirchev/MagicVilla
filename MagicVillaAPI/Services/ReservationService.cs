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

        public ReservationService(IReservationRepository reservationRepository,
            IUserService userService, 
            IVillaService villaService,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _userService = userService;
            _villaService = villaService;
            _mapper = mapper;
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

            return await _reservationRepository.CreateAsync(reservation);
        }

        public Task DeleteReservationByIdAsync(Guid reservationId)
        {
            throw new NotImplementedException();
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
