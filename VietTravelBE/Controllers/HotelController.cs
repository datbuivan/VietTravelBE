using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;

namespace VietTravelBE.Controllers
{
    public partial class HotelController
    {
        [HttpGet("rooms")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> getRoom(int hotelId)
        {
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
