using MagicVillaAPI.Models;

namespace MagicVillaAPI.Services
{
    public interface IAuditRecordService
    {
        public Task<AuditRecord> CreateAsync(object entity, ActionType action, Guid performedBy, DateTime timestamp, string details);
        public Task<IEnumerable<AuditRecord>> GetAllAsync();
        public Task<AuditRecord> GetByIdAsync(Guid id);
        public Task<IEnumerable<AuditRecord>> GetAllByUserId(Guid userId);
    }
}
