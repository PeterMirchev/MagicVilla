using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;

namespace MagicVillaAPI.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationCreateRequest request);
        Task<Reservation> UpdateReservationAsync(ReservationUpdateRequest request);
        Task<Reservation> PayReservationAsync(ReservationPayRequest request);
        Task<IEnumerable<Reservation>> GetAllReservationsByUserIdAsync(Guid userId);
        Task<Reservation> GetReservationByIdAsync(Guid reservationId);
        Task DeleteReservationByIdAsync(Guid reservationId);
    }
}
