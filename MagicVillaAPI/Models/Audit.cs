using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVillaAPI.Models
{
    public class Audit<T>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public ActionType Action { get; set; }
        [NotMapped]
        public T Entity { get; set; }
        [Required]
        public Guid PerformedBy { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public string Details { get; set; }
        // Serialized data for the entity
        [Column("EntityData")]
        public string EntityData { get; set; }

        // Parameterless constructor for EF
        public Audit()
        {
        }
        public Audit(T entity, ActionType action, Guid performedBy, DateTime timestamp, string details)
        {
            Entity = entity;
            Action = action;
            PerformedBy = performedBy;
            Timestamp = timestamp;
            Details = details;
        }
    }
}
