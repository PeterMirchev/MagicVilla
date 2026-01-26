using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _db;

        public ReservationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteAsync(Guid reservationId)
        {
            await _db.Reservations
            .Where(r => r.Id == reservationId)
            .ExecuteDeleteAsync();
        }

        public async Task<ICollection<Reservation>> GetAllByUserIdAsync(Guid userId)
        {
            return await _db.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(Guid reservationId)
        {
            return await _db.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            await _db.SaveChangesAsync();
            return reservation;
        }
    }
}
