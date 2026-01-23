using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models;

public class Villa
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Required]
    public decimal PricePerDay { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [Required]
    public DateTime CreatedDate { get; set; }
}
