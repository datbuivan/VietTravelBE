using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Services;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class HotelController : BaseApiWithSpecController<Hotel, HotelCreateDto, HotelDto>
    {
        private readonly RoomService _roomService;
        public HotelController(IGenericRepository<Hotel> repo, IUnitOfWork unit, IMapper mapper, RoomService roomService)
            : base(repo, unit, mapper) 
        {
            _roomService = roomService;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class TourController : BaseApiWithSpecController<Tour, TourCreateDto, TourDto>
    {
        private readonly TourService _tourService;
        private readonly IFileValidationService _fileValidationService;
        public TourController(IGenericRepository<Tour> repo, IUnitOfWork unit, IMapper mapper, IFileValidationService fileValidationService, TourService tourService)
            : base(repo, unit, mapper) 
        {
            _tourService = tourService;
            _fileValidationService = fileValidationService;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class RoomController : BaseApiController<Room, RoomDto, RoomDto>
    {
        
        public RoomController(IGenericRepository<Room> repo, IUnitOfWork unit, IMapper mapper)
            : base(repo, unit, mapper) 
        { 
            
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class CityController : BaseApiController<City, CityCreateDto, CityDto>
    {
        private readonly ICityService _cityService;
        public CityController(IGenericRepository<City> repo, IUnitOfWork unit, IMapper mapper, ICityService cityService)
            : base(repo, unit, mapper) 
        {
            _cityService = cityService;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class RegionController : BaseApiController<Region, RegionDto, RegionDto>
    {
        public RegionController(IGenericRepository<Region> repo, IUnitOfWork unit, IMapper mapper)
            : base(repo, unit, mapper) { }
    }

}
