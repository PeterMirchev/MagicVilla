using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto.Wallet
{
    public class WalletCreateRequest
    {
        [Required(ErrorMessage ="Wallet name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
