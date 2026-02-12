using ServiceStack.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.DiscountRequests
{
    public class DiscountRequest : Request
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string ReasonForRequest { get; set; } = string.Empty;
    }
}
