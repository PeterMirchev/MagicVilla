using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto.Wallet
{
    public class WalletUpdateRequest
    {
        [Required(ErrorMessage = "Wallet name is required")]
        public string Name { get; set; }
    }
}
