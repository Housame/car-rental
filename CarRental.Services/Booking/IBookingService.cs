using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Booking
{
    public interface IBookingService
    {
        Task <IEnumerable<Data.Models.Reservation>> GetAllReservations();
        //Data.Models.Booking GetReservationByBookingNum(int bookingNum);
        bool ValidateByBookingNum(int bookingsNum);
        int GetNewBookingNum();
        Task <ServiceResponse<Data.Models.Reservation>> SetReservation(Data.Models.DTO.ReservationDTO reservation);

        Task<ServiceResponse<Data.Models.Booking>> SubmitReturnedCar(Data.Models.DTO.BookingDTO booking);
        double GetPrice(Data.Models.DTO.BookingDTO returnedCar);
        Task SetMockData();
        List<Data.Models.Car> GetAvailableCars();
    }
}
