using MagicVillaAPI.Models;

namespace MagicVillaAPI.Repositories
{
    public interface IAudutRecrdRepository
    {
        public Task<AuditRecord> CreateAsync(AuditRecord record);
        public Task<IEnumerable<AuditRecord>> GetAllAsync();
        Task<AuditRecord?> GetByIdAsync(Guid id);
        Task<IEnumerable<AuditRecord>> GetAllByUserId(Guid userId);
    }
}
