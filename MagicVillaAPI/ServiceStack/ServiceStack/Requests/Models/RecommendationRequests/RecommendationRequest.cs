using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.RecommendationRequests
{
    public class RecommendationRequest : Request
    {
        [Required]
        public string Information { get; set; } = string.Empty;
    }
}
