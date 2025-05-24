using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class RoomController : BaseApiController<Room, RoomDto, RoomDto>
    {
        private readonly RoomService _roomService;
        public RoomController(IGenericRepository<Room> repo, IUnitOfWork unit, IMapper mapper, RoomService roomService)
            : base(repo, unit, mapper)
        {
            _roomService = roomService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("rooms")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> getRoom(int hotelId)
        {
            if (hotelId <= 0)
            {
                return BadRequest(new ApiResponse<RoomDto>(400, "Invalid hotelId"));
            }

            try
            {
                var room = await _roomService.GetRoomByHotelId(hotelId);
                if (room == null)
                {
                    return NotFound(new ApiResponse<RoomDto>(404, "Not found room by hotelId"));
                }
                return Ok(new ApiResponse<IReadOnlyList<RoomDto>>(200, data: room));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));

            }
        }
    }
}
