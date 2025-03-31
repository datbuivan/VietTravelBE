using AutoMapper;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
        }
    }
}
