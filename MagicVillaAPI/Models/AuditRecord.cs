namespace MagicVillaAPI.Models
{
    public class AuditRecord : Audit<object>
    {
        // Parameterless constructor for EF
        public AuditRecord() : base() { }

        // Parameterized constructor for your own logic
        public AuditRecord(object entity, ActionType action, Guid performedBy, DateTime timestamp, string details) 
            : base(entity, action, performedBy, timestamp, details)
        {
        }
    }
}
