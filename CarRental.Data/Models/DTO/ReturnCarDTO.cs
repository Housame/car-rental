using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Models.DTO
{
    [NotMapped]
    public class ReturnCarDTO
    {
        public int BookingNum { get; set; }
        public string IncomingDate { get; set; }
        public int IncomingMileage { get; set; }
    }
}
