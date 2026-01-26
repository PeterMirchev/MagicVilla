using MagicVillaAPI.Models;

namespace MagicVillaAPI.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<ICollection<Reservation>> GetAllByUserIdAsync(Guid userId);
        Task<Reservation?> GetByIdAsync(Guid reservationId);
        Task<Reservation> UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid reservationId);
    }
}
