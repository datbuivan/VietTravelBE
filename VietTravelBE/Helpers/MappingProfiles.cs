using AutoMapper;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Hotel, HotelCreateDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<TourStartDate, TourStartDateDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Rooms.Min(room => room.Price)));
            CreateMap<Hotel, Hotel>()
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Rooms != null && src.Rooms.Any()
                    ? src.Rooms.Min(r => r.Price): 0));
        }
    }
}
