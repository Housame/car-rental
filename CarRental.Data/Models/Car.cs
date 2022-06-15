using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string RegNum { get; set; }
        public int CarCategoryId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
