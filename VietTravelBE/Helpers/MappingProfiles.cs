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
            CreateMap<TourCreateDto, Tour>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            // Nếu bạn cần mapping ngược lại từ Tour sang TourCreateDto
            CreateMap<Tour, TourCreateDto>()
                .ForMember(dest => dest.PrimaryImage, opt => opt.Ignore());

            CreateMap<TourStartDate, TourStartDateDto>().ReverseMap();

            CreateMap<HotelCreateDto, Hotel>().ReverseMap();
            CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Rooms.Min(room => room.Price)));
            CreateMap<Hotel, Hotel>()
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Rooms != null && src.Rooms.Any()
                    ? src.Rooms.Min(r => r.Price): 0));

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


        }
    }
}
