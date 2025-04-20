using AutoMapper;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class HotelService
    {
        private readonly IGenericRepository<Hotel> _hotelRepo;
        private readonly IMapper _mapper;

        public HotelService(IGenericRepository<Hotel> hotelsRepo, IMapper mapper)
        {
            _hotelRepo = hotelsRepo;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<HotelDto>> GetHotels(SpecParams? specParams)
        {
            var spec = specParams == null
                ? new BaseSpecification<Hotel>()
                : new Specification<Hotel>(specParams); ;
            var hotels = await _hotelRepo.ListAsync(spec);
            var hotelDtos = _mapper.Map<IReadOnlyList<HotelDto>>(hotels);
            if (specParams?.Sort == "priceAsc")
            {
                hotelDtos = hotelDtos.OrderBy(dto => dto.Price).ToList();
            }
            else if (specParams?.Sort == "priceDesc")
            {
                hotelDtos = hotelDtos.OrderByDescending(dto => dto.Price).ToList();
            }
            else
            {
                hotelDtos = hotelDtos.OrderBy(dto => dto.Name).ToList(); // Sắp xếp mặc định theo Name
            }

            return hotelDtos;
        }
    }
}
