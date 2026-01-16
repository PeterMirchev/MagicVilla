using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
