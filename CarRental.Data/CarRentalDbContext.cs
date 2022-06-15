using CarRental.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext() { }

        public CarRentalDbContext(DbContextOptions options) : base(options){ }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Car> Car { get; set; }

    }
}
