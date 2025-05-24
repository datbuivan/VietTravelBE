using AutoMapper;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<CityCreateDto, City>().ReverseMap();
            
            CreateMap<Room, RoomDto>().ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<TourCreateDto, Tour>().ReverseMap();

            CreateMap<Tour, TourCreateDto>();

            CreateMap<TourStartDate, TourStartDateDto>().ReverseMap();

            CreateMap<HotelCreateDto, Hotel>().ReverseMap();
            CreateMap<Hotel, HotelDto>();
            CreateMap<Hotel, Hotel>();

            CreateMap<TourSchedule, TourScheduleDto>().ReverseMap();
            CreateMap<TourScheduleDto, TourSchedule>().ReverseMap();

            CreateMap<TourSchedule, TourScheduleCreateDto>().ReverseMap();
            CreateMap<TourScheduleCreateDto, TourSchedule>().ReverseMap();


            CreateMap<TourStartDate, TourStartDateDto>().ReverseMap();
            CreateMap<TourStartDateDto, TourStartDate>().ReverseMap();

            CreateMap<TourStartDate, TourStartDateCreateDto>().ReverseMap();
            CreateMap<TourStartDateCreateDto, TourStartDate>().ReverseMap();

            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<ImageDto, Image>().ReverseMap();

            CreateMap<TourFavorite, TourFavoriteDto>().ReverseMap();
            CreateMap<TourFavoriteDto, TourFavorite>().ReverseMap();


        }
    }
}
