using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;

namespace VietTravelBE.Controllers
{
    public partial class HotelController
    {
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<HotelDto>>> Create([FromBody] HotelCreateDto dto)
        {
            return await base.Create(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<HotelDto>>> Update(int id, [FromBody] HotelCreateDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            return await base.Delete(id);
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
