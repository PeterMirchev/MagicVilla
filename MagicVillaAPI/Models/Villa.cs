using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models
{
    public class Villa
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
