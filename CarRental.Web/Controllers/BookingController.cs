using Microsoft.AspNetCore.Mvc;
using CarRental.Services.Booking;
using CarRental.Web.Serialization;
using Microsoft.AspNetCore.Cors;
using CarRental.Data.Models.DTO;


namespace CarRental.Web.Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpGet("/api/bookings")]
        public async Task<IActionResult> GetBookings()
        {
            _logger.LogInformation("Getting all bookings");
            var bookings = await _bookingService.GetAllReservations();
            //var bookingViewModel = bookings.Select(BookingMapper.SerializeModel);
            return Ok(bookings);
        }
        [HttpPost("/api/newReservation")]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDTO reservation)
        {
            _logger.LogInformation("Add new reservation");
            await _bookingService.SetReservation(reservation);
            
            return Ok();

        }
        [HttpPost("/api/submitReturnedCar")]
        public async Task<IActionResult> SubmitReturnedCar([FromBody] BookingDTO booking)
        {
            _logger.LogInformation("Submit returned car");
            await _bookingService.SubmitReturnedCar(booking);
            var price = _bookingService.GetPrice(booking);
            return Ok(price);
        }


        //[HttpGet("/api/getprice/")]
        //public ActionResult GetPrice([FromQuery] ReturnCarDTO returnedCar)
        //{ 
        //    _logger.LogInformation("Getting bookings price from ReturnCarDTO:", returnedCar);
        //    var price = _bookingService.GetPrice(returnedCar);
        //    //var bookingViewModel = bookings.Select(BookingMapper.SerializeModel);
        //    return Ok(price);
        //}
        [HttpGet("/api/newbookingNum")]
        public ActionResult GetNewBookingNum()
        {
            _logger.LogInformation("Get a New BookingNum");
            var newBookingNum = _bookingService.GetNewBookingNum();
            //var bookingViewModel = bookings.Select(BookingMapper.SerializeModel);
            return Ok(newBookingNum);
        }
        [HttpGet("/api/availablecars")]
        public ActionResult GetAvailableCars()
        {
            _logger.LogInformation("Get Available Cars");
            var cars = _bookingService.GetAvailableCars();


            return Ok(cars);
        }
        [HttpGet("/api/setmockdata")]
        public ActionResult SetMockData()
        {
            _logger.LogInformation("Setting mock data");
            var booking = _bookingService.SetMockData();


            return Ok(booking);
        }
    }
}
