using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;
using ServiceStack.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models
{
    public abstract class Request
    {
        [PrimaryKey]
        [AutoId]
        public Guid Id { get; set; }

        [ForeignKey(typeof(UserLite), OnDelete = "CASCADE")]
        public Guid UserId { get; set; }

        [Reference]
        public UserLite? User { get; set; }

        [Required]
        public RequestType RequestType { get; set; }

        [Required]
        public RequestStatus RequestStatus { get; set; }

        public string? ApprovedDeniedReason { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
