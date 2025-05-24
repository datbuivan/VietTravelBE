using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Dtos;

namespace VietTravelBE.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public partial class RoomController : BaseApiController<Room, RoomDto, RoomDto>
    //{
    //    public RoomController(IGenericRepository<Room> repo, IUnitOfWork unit, IMapper mapper)
    //        : base(repo, unit, mapper) { }
    //}

    [Route("api/[controller]")]
    [ApiController]
    public partial class RegionController : BaseApiController<Region, RegionDto, RegionDto>
    {
        public RegionController(IGenericRepository<Region> repo, IUnitOfWork unit, IMapper mapper)
            : base(repo, unit, mapper) { }
    }

}
