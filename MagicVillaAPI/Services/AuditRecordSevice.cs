using System.Text.Json;
using MagicVillaAPI.Models;
using MagicVillaAPI.Repositories;

namespace MagicVillaAPI.Services
{
    public class AuditRecordSevice : IAuditRecordService
    {
        private readonly IAudutRecrdRepository _auditRecordRepository;

        public AuditRecordSevice(IAudutRecrdRepository auditRecordRepository)
        {
            _auditRecordRepository = auditRecordRepository;
        }

        public async Task<AuditRecord> CreateAsync(object entity, ActionType action, Guid performedBy, DateTime timestamp, string details)
        {
            var record = new AuditRecord(entity, action, performedBy, timestamp, details);
            record.EntityData = JsonSerializer.Serialize(entity);

            await _auditRecordRepository.CreateAsync(record);
            return record;
        }

        public async Task<IEnumerable<AuditRecord>> GetAllAsync()
        {
            return await _auditRecordRepository.GetAllAsync();
        }

        public async Task<IEnumerable<AuditRecord>> GetAllByUserId(Guid userId)
        {
            return await _auditRecordRepository.GetAllByUserId(userId);
        }

        public async Task<AuditRecord> GetByIdAsync(Guid id)
        {
            var record = await _auditRecordRepository.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException($"AuditRecord with ID '{id}' not found.");

            // Deserialize EntityData to the correct type
            record.Entity = JsonSerializer.Deserialize<object>(record.EntityData);

            return record;
        }
    }
}
