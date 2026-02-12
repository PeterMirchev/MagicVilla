using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.SupportRequests
{
    public class SupportRequest : Request
    {
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
