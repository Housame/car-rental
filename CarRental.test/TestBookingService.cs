using CarRental.Data;
using CarRental.Services.Booking;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.test
{
    public class TestBookingService
    {
        [Fact]
        public void BookingService_GetAllReservation_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CarRentalDbContext>()
                 .UseInMemoryDatabase("gets_all").Options;

            using var context = new CarRentalDbContext(options);

            var sut = new BookingService(context);

            sut.SetReservation(new Data.Models.Reservation { Id = 001 });
            sut.SetReservation(new Data.Models.Reservation { Id = 002 });
            sut.SetReservation(new Data.Models.Reservation { Id = 003 });

            var allReservations = sut.GetAllReservations();

            allReservations.Count.Should().Be(3);
        }

        [Fact]
        public void BookingService_SetReservation_GivenNewReservationObject()
        {
            var options = new DbContextOptionsBuilder<CarRentalDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new CarRentalDbContext(options);
            var sut = new BookingService(context);

            sut.SetReservation(new Data.Models.Reservation { Id = 001 });
            context.Reservation.Single().Id.Should().Be(001);

        }

        [Fact]
        public void BookingService_GetLatestReservations_GivenNewReservationObject()
        {

        }
    }
}
