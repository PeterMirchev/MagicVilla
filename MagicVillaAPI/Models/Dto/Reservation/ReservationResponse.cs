namespace MagicVillaAPI.Models.Dto.Reservation
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public int Days { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public Guid VillaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
