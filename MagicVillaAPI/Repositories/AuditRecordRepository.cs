
using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositories
{
    public class AuditRecordRepository : IAudutRecrdRepository
    {
        private readonly AppDbContext _db;

        public AuditRecordRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AuditRecord> CreateAsync(AuditRecord record)
        {
            _db.AuditRecords.Add(record);
            await _db.SaveChangesAsync();
            return record;
        }

        public async Task<IEnumerable<AuditRecord>> GetAllAsync()
        {
            return await _db.AuditRecords.ToListAsync();
        }

        public async Task<IEnumerable<AuditRecord>> GetAllByUserId(Guid userId)
        {
            return await _db.AuditRecords
            .Where(a => a.PerformedBy == userId)
            .ToListAsync();
        }

        public async Task<AuditRecord?> GetByIdAsync(Guid id)
        {
            return await _db.AuditRecords.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
