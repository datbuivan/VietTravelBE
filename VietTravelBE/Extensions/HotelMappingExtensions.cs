using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Extensions
{
    public static class HotelMappingExtensions
    {
        public static Hotel CreateDtoToHotel(this HotelCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Data DTO is not null.");

            return new Hotel
            {
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                TitleIntroduct = dto.TitleIntroduct,
                ContentIntroduct = dto.ContentIntroduct,
                CityId = dto.CityId,
                City = null,
                Rooms = new List<Room>(),
                Reviews = new List<Review>(),
                Bookings = new List<Booking>(),
                Images = new List<Image>()
            };
        }

        public static HotelDto ToHotelDto(this Hotel hotel)
        {
            if (hotel == null)
                throw new ArgumentNullException(nameof(hotel), "Hotel is not null.");

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                PhoneNumber = hotel.PhoneNumber,
                TitleIntroduct = hotel.TitleIntroduct,
                ContentIntroduct = hotel.ContentIntroduct,
                CityId = hotel.CityId,
                Images = hotel.Images?.Select(image => new ImageDto
                {
                    Id = image.Id,
                    Url = image.Url,
                    IsPrimary = image.IsPrimary
                }).ToList() ?? new List<ImageDto>()
            };
        }

        public static void UpdateDtoToHotel(this Hotel hotel , HotelCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Data DTO is not null.");

            hotel.Name = dto.Name;
            hotel.Address = dto.Address;
            hotel.PhoneNumber = dto.PhoneNumber;
            hotel.TitleIntroduct = dto.TitleIntroduct;
            hotel.ContentIntroduct = dto.ContentIntroduct;
            hotel.CityId = dto.CityId;
            
        }
    }
}
