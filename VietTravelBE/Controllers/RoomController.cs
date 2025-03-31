using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IGenericRepository<Room> _roomsRepo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public RoomController(IUnitOfWork unit, IMapper mapper, IGenericRepository<Room> roomsRepo)
        {
            _unit = unit;
            _mapper = mapper;
            _roomsRepo = roomsRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Room>>> Getrooms()
        {

            try
            {
                if (_roomsRepo == null)
                {
                    return StatusCode(500, new ApiResponse(500, "Repository is not initialized"));
                }
                var rooms = await _roomsRepo.ListAllAsync();
                if (rooms == null || !rooms.Any())
                    return NotFound(new ApiResponse(404, "No rooms found"));
                var data = _mapper.Map<IReadOnlyList<Room>, IReadOnlyList<RoomDto>>(rooms);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Internal Server Error: {ex.Message}"));
            }
        }

        [HttpGet("{id:int}")] // api/rooms/2
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var Room = await _roomsRepo.GetByIdAsync(id);

            if (Room == null) return NotFound(new ApiResponse(404, "Not found room data"));

            var data = _mapper.Map<Room, RoomDto>(Room);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] RoomDto RoomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid Room data"));

            var Room = _mapper.Map<Room>(RoomDto);

            _roomsRepo.Add(Room);
            await _unit.Complete();

            var createdRoom = _mapper.Map<RoomDto>(Room);

            return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.Id }, createdRoom);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDto>> UpdateRoom(int id, RoomDto RoomDto)
        {
            if (id != RoomDto.Id)
                return BadRequest(new ApiResponse(400, "Room ID mismatch"));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid Room data"));

            var room = await _roomsRepo.GetByIdAsync(id);

            if (room == null)
                return NotFound(new ApiResponse(404, "Room not found"));

            _mapper.Map(RoomDto, room);
            _roomsRepo.Update(room);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "Room updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            var room = await _roomsRepo.GetByIdAsync(id);
            if (room == null)
                return NotFound(new ApiResponse(404, "Room not found"));

            _roomsRepo.Delete(room);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "Room delete successfully"));
        }

    }
}
