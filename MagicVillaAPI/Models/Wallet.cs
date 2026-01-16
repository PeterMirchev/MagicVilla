using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models
{
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
