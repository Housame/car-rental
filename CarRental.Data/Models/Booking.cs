using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Models
{
    public class Booking
    {

        public int Id { get; set; }

        public int BookingNum { get; set; }

        public long SocialNum { get; set; }

        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

        public DateTime IncomingDate { get; set; }
        public int IncomingMileage { get; set; }
        
        public double Price { get; set; }
    }

}
