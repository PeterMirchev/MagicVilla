using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto.Reservation
{
    public class ReservationCreateRequest
    {
        public string Note { get; set; }
        [Required(ErrorMessage = "Starting date is required")]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "Ending date is required")]
        public DateTime To { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Villa ID is required")]
        public Guid VillaId { get; set; }
    }
}
