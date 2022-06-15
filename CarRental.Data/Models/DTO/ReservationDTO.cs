using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Data.Models.DTO
{
    [NotMapped]
    public class ReservationDTO
    {
        public int BookingNum { get; set; }
        public string OutgoingDate { get; set; }
        public string OutgoingMileAge { get; set; }
        public int CarId { get; set; }
        public string SocialNumber { get; set; }
    }
}
