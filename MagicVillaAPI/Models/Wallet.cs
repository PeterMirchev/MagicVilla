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

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
