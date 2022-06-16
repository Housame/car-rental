using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly CarRentalDbContext _db;
        public BookingService(CarRentalDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Retrieve all bookings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Data.Models.Reservation>> GetAllReservations()
        {
            var bookings = new List<Data.Models.Reservation>();
            try
            {
                bookings = await _db.Reservation.ToListAsync();
            }
            catch (Exception e)
            {

                throw;
            }
            
            return bookings;
        }


        /// <summary>
        /// Add a new reservation to the DB
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Data.Models.Reservation>> SetReservation(Data.Models.DTO.ReservationDTO reservation)
        {
            var newReserv = new Reservation
            {
                BookingNum = reservation.BookingNum,
                OutgoingDate = DateTime.Parse(reservation.OutgoingDate),
                OutgoingMileAge = Int32.Parse(reservation.OutgoingMileAge),
                CarId = reservation.CarId,
                SocialNum = Int32.Parse(reservation.SocialNumber),
                IsReturned = false,
                

            };
            try
            {
                await _db.Reservation.AddAsync(newReserv);
                // MAKE THE CAR NOT AVAILABLE AT THE DB
                _db.Car.First(c => c.Id == newReserv.CarId).IsAvailable = false;
                await _db.SaveChangesAsync();
                
                return new ServiceResponse<Data.Models.Reservation>
                {
                    Data = newReserv,
                    Time = DateTime.UtcNow,
                    Message = "New Booking made",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {

                return new ServiceResponse<Data.Models.Reservation>
                {
                    Data = newReserv,
                    Time = DateTime.UtcNow,
                    Message = "Error when making reservation" + e.StackTrace,
                    IsSuccess = false
                };
            }

        }

        /// <summary>
        /// Calculatiing & returning the price for rented car when returned
        /// </summary>
        /// <param name="returnedCar"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public double GetPrice(BookingDTO returnedCar)
        {
            var carTypeId = _db.Reservation.First(r=> r.BookingNum == returnedCar.BookingNum).Car.CarCategoryId;

            return Calculate(returnedCar, carTypeId);
        }

        private double Calculate(BookingDTO returnedCar, int carTypeId)
        {
            var reservation = _db.Reservation.FirstOrDefault(r => r.BookingNum == returnedCar.BookingNum);

            var startDate = reservation.OutgoingDate;
            var startMileAge = reservation.OutgoingMileAge;
            // Temporary equation due a bug in frontend
            var totDays = (Convert.ToDateTime(returnedCar.IncomingDate) - startDate).TotalDays;
            var totMiles = (Int32.Parse(returnedCar.IncomingMileage) - startMileAge);

            switch (carTypeId)
            {
                case 1:
                    {
                        return 600 * totDays;
                    }
                case 2:
                    {
                        return (600 * totDays * 1.3) + (10 * totMiles);
                    }
                case 3:
                    {
                        return (6000 * totDays * 1.5) + (10 * totMiles * 1.5);
                    }
                default: return 0;
            }
        }

        /// <summary>
        /// Retrieve a booking by booking number
        /// </summary>
        /// <param name="bookingNum"></param>
        /// <returns></returns>
        public bool ValidateByBookingNum(int bookingsNum)
        {
            var reservation = _db.Reservation.Any(r => r.BookingNum == bookingsNum);
            return reservation;
        }




        private ServiceResponse<Car> SetCar(Car car)
        {
            try
            {
                _db.Car.Add(car);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Car>
                {
                    Data = car,
                    Time = DateTime.UtcNow,
                    Message = "New Booking made",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<Data.Models.Booking>> SubmitReturnedCar(Data.Models.DTO.BookingDTO booking)
        {
            var reservation = await _db.Reservation.FirstAsync(r => r.BookingNum == booking.BookingNum);
            var submittedBooking = new Data.Models.Booking()
            {
                BookingNum = booking.BookingNum,
                IncomingDate = DateTime.Parse(booking.IncomingDate),
                IncomingMileage = Int32.Parse(booking.IncomingMileage),
                SocialNum = reservation.SocialNum,
                ReservationId = reservation.Id,
                Reservation = reservation
            };
            try
            {
                reservation.IsReturned = true;
                await _db.AddAsync(booking);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Data.Models.Booking>
                {
                    Data = submittedBooking,
                    Time = DateTime.UtcNow,
                    Message = "New Booking made",
                    IsSuccess = true
                };
            }   
            catch (Exception e)
            {

                return new ServiceResponse<Data.Models.Booking>
                {
                    Data = submittedBooking,
                    Time = DateTime.UtcNow,
                    Message = "Error when submitting booking" + e.StackTrace,
                    IsSuccess = true
                };
            }


        }

        /// <summary>
        /// Find te latest bookingnumber and add 1 for making a new reservation
        /// </summary>
        /// <returns></returns>
        public int GetNewBookingNum()
        {
            var latestookingNum = _db.Reservation.OrderBy(b=>b.BookingNum).Last().BookingNum;
            if (latestookingNum != null)
            {
                return latestookingNum + 1;
            }
            else return 1001;
            
        }

        public List<Car> GetAvailableCars()
        {
            var cars = _db.Car.Where(c => c.IsAvailable).ToList();
            return cars;
        }

        /// <summary>
        /// Fix async/await to connect the car to reservation ---------------   Data/JSON file?
        /// </summary>
        /// <returns></returns>
        public async Task SetMockData()
        {
            var carList = new List<Car>() { 
                new Car { 
                    CarCategoryId = 1, RegNum="AAA100", IsAvailable=true
                },
                new Car {
                    CarCategoryId = 2, RegNum="BBB200", IsAvailable=true
                },
                new Car {
                    CarCategoryId = 3, RegNum="CCC300", IsAvailable=true
                },
                new Car {
                    CarCategoryId = 1, RegNum="GGG100", IsAvailable=true
                },
                new Car {
                    CarCategoryId = 2, RegNum="HHH200", IsAvailable=true
                },
                new Car {
                    CarCategoryId = 3, RegNum="LLL300", IsAvailable=true
                }
            };
            try
            {
                await _db.Car.AddRangeAsync(carList);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            var reservationList = new List<Reservation>()
            {
                new Reservation
                {
                    BookingNum = 1010, IsReturned = false, OutgoingDate = new DateTime(2022,06,03, 11,00,00), SocialNum = 199001011122, OutgoingMileAge = 1000,
                    Car = new Car
                    {
                        CarCategoryId = 1, RegNum="DDD100", IsAvailable=false
                    }
                },
                new Reservation
                {
                    BookingNum = 1011, IsReturned = false, OutgoingDate = new DateTime(2022,06,04, 11,00,00), SocialNum = 199102022233, OutgoingMileAge = 1000,
                    Car = new Car
                    {
                        CarCategoryId = 2, RegNum="EEE200", IsAvailable=false
                    }
                },
                new Reservation
                {
                    BookingNum = 1012, IsReturned = false, OutgoingDate = new DateTime(2022,06,05, 11,00,00), SocialNum = 199203033344, OutgoingMileAge = 1000,
                    Car = new Car
                    {
                        CarCategoryId = 3, RegNum="FFF300", IsAvailable=false
                    }
                }
            };
            try
            {
                await _db.Reservation.AddRangeAsync(reservationList);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
