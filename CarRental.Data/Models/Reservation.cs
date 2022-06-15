using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public long SocialNum { get; set; }
        public virtual Car Car { get; set; }
        public int BookingNum { get; set; }
        public DateTime OutgoingDate { get; set; }
        public int OutgoingMileAge { get; set; }
        public bool IsReturned { get; set; }
    }
}
