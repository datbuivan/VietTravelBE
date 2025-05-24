using Humanizer;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Extensions
{
    public static class TourMappingExtensions
    {
        public static Tour CreateDtoToTour(this TourCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "data DTO is not null.");

            return new Tour
            {
                Name = dto.Name,
                Price = dto.Price,
                ChildPrice = dto.ChildPrice,
                SingleRoomSurcharge = dto.SingleRoomSurcharge,
                CityId = dto.CityId,
                City = null,
                TourSchedules = new List<TourSchedule>(),
                TourStartDates = new List<TourStartDate>(),
                Reviews = new List<Review>(),
                Bookings = new List<Booking>(),
                Images = new List<Image>()
            };
        }

        public static TourDto ToTourDto(this Tour tour)
        {
            if (tour == null)
                throw new ArgumentNullException(nameof(tour), "tour is not null.");

            return new TourDto
            {
                Name = tour.Name,
                Price = tour.Price,
                ChildPrice = tour.ChildPrice,
                SingleRoomSurcharge = tour.SingleRoomSurcharge,
                CityId = tour.CityId,
                Images = tour.Images?.Select(image => new ImageDto
                {
                    Id = image.Id,
                    Url = image.Url,
                    IsPrimary = image.IsPrimary
                }).ToList() ?? new List<ImageDto>()
            };
        }

        public static void UpdateDtoToTour(this Tour tour,  TourCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Data DTO is not null.");

            tour.Name = dto.Name;
            tour.Price = dto.Price;
            tour.ChildPrice = dto.ChildPrice;
            tour.SingleRoomSurcharge = dto.SingleRoomSurcharge;
            tour.CityId = dto.CityId;
        }
    }
}
