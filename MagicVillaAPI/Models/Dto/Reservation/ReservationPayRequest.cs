using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto.Reservation
{
    public class ReservationPayRequest
    {
        [Required(ErrorMessage = "Reservation ID is required.")]
        public Guid ReservationId;
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId;
        [Required(ErrorMessage = "Wallet ID is required.")]
        public Guid WalletId;
    }
}
