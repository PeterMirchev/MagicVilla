using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models;

public class Villa
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Models.Reservation> Reservations { get; set; } = new List<Models.Reservation>();

    [Required]
    public DateTime CreatedDate { get; set; }
}
