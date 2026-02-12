using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.DiscountRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.RecommendationRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.SupportRequests;
using ServiceStack.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Model
{
    public class UserLite
    {
        [PrimaryKey]
        [AutoId]
        public Guid Id { get; set; }

        public string FirstName {  get; set; }

        public string LastName { get; set; }

        [Unique]
        public string PhoneNumber { get; set; }

        [Unique]
        public string Email { get; set; }

        [Reference]
        public List<DiscountRequest>? DiscountRequests { get; set; }

        [Reference]
        public List<SupportRequest>? SupportRequests { get; set; }

        [Reference]
        public List<RecommendationRequest>? RecommendationRequests { get; set; }

        [Required]
        public Role Role { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
