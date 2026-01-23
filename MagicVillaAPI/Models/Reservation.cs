using System.ComponentModel.DataAnnotations;
using MagicVillaAPI.Models;

namespace MagicVillaAPI.Models;
public class Reservation
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public int Days { get; set; }
    [Required]
    public DateTime From { get; set; }
    [Required]
    public DateTime To { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid VillaId { get; set; }
    public Villa Villa { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
