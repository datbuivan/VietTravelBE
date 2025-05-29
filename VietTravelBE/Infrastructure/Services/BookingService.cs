using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepo;
        private readonly IUserRepository _userRepo;
        private readonly IHotelRepository _hotelRepo;
        private readonly ITourRepository _tourRepo;
        private readonly IStartDateRepository _startDateRepo;
        private readonly IUnitOfWork _unit;
        public BookingService(
            IGenericRepository<Booking> bookingRepo,
            IUserRepository userRepo, 
            IHotelRepository hotelRepo,
            ITourRepository tourRepo,
            IStartDateRepository startDateRepo,
            IUnitOfWork unit)
        {
            _bookingRepo = bookingRepo;
            _userRepo = userRepo;
            _hotelRepo = hotelRepo;
            _tourRepo = tourRepo;
            _startDateRepo = startDateRepo;
            _unit = unit;
        }
        public async Task<BookingDto> CreateBookingAsync(BookingAndPayCreateDto request)
        {
            
                try
                {
                    var userExists = await _userRepo.UserExistsAsync(request.UserId);
                    if (!userExists)
                        throw new Exception("User not found.");

                    if (request.Type == BookingType.Hotel )
                    {
                        if (!request.HotelId.HasValue || !request.HotelCheckInDate.HasValue || !request.HotelCheckOutDate.HasValue)
                            throw new Exception("HotelId, HotelCheckInDate, and HotelCheckOutDate are required for Hotel booking.");
                        if (!await _hotelRepo.HotelExistsAsync(request.HotelId.Value))
                            throw new Exception("Hotel not found.");
                        if (request.HotelCheckInDate >= request.HotelCheckOutDate)
                            throw new Exception("Check-out date must be after check-in date.");
                    }
                    else if (request.Type == BookingType.Tour)
                    {
                        if (!request.TourId.HasValue || !request.TourStartDateId.HasValue)
                            throw new Exception("TourId and TourStartDateId are required for Tour booking.");
                        if (!await _tourRepo.TourExistsAsync(request.TourId.Value))
                            throw new Exception("Tour not found.");
                        if (!await _startDateRepo.StartDateExistsAsync(request.TourStartDateId.Value))
                            throw new Exception("Tour start date not found.");
                    }

                    var booking = new Booking
                    {
                        TotalPrice = request.TotalPrice,
                        BookingDate = DateTime.Now,
                        Type = request.Type,
                        UserId = request.UserId,
                        Adults = request.Adults,
                        Children = request.Children,
                        TotalPeople = request.Adults + request.Children,
                        HotelId = request.HotelId,
                        HotelCheckInDate = request.HotelCheckInDate,
                        HotelCheckOutDate = request.HotelCheckOutDate,
                        TourId = request.TourId,
                        TourStartDateId = request.TourStartDateId
                    };

                    _bookingRepo.Add(booking);
                    await _unit.Complete();

                    var bookingDto = new BookingDto
                    {
                        Id = booking.Id,
                        TotalPrice = booking.TotalPrice,
                        Type = booking.Type,
                        UserId = booking.UserId,
                        Adults = booking.Adults,
                        Children = booking.Children,
                        TotalPeople = booking.TotalPeople,
                        HotelId = booking.HotelId,
                        HotelCheckInDate = booking.HotelCheckInDate,
                        HotelCheckOutDate = booking.HotelCheckOutDate,
                        TourId = booking.TourId,
                        TourStartDateId = booking.TourStartDateId,
                    };

                    
                    return bookingDto;
                }
                catch (Exception ex)
                {
                    throw new Exception( $"An error occurred while creating the booking" + ex.Message);
                }

        }
    }
}
