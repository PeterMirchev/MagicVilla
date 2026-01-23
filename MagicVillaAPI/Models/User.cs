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
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
