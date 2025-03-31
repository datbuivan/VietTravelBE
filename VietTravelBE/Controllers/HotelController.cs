using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : BaseApiController
    {
        private readonly IGenericRepository<Hotel> _hotelsRepo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public HotelController(IUnitOfWork unit, IMapper mapper, IGenericRepository<Hotel> hotelsRepo)
        {
            _unit = unit;
            _mapper = mapper;
            _hotelsRepo = hotelsRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Hotel>>> GetHotels(
        [FromQuery] SpecParams specParams)
        {
            var spec = new Specification(specParams);
            try
            {
                var hotels = await _hotelsRepo.ListAsync(spec);
                var data = _mapper.Map<IReadOnlyList<Hotel>, IReadOnlyList<HotelDto>>(hotels);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")] // api/hotels/2
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(id);

            if (hotel == null) return NotFound(new ApiResponse(404, "Not found hotel data"));

            var data = _mapper.Map<Hotel, HotelDto>(hotel);

            return Ok(data) ;
        }

        [HttpPost]
        public async Task<ActionResult<HotelDto>> CreateHotel([FromBody] HotelDto hotelDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid hotel data"));

            var hotel = _mapper.Map<Hotel>(hotelDto);

            _hotelsRepo.Add(hotel);
            await _unit.Complete();

            var createdHotel = _mapper.Map<HotelDto>(hotel);

            return CreatedAtAction(nameof(GetHotel), new { id = createdHotel.Id }, createdHotel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDto>> UpdateHotel(int id, HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
                return BadRequest(new ApiResponse(400, "Hotel ID mismatch"));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid hotel data"));

            var hotel = await _hotelsRepo.GetByIdAsync(id);

            if (hotel == null)
                return NotFound(new ApiResponse(404, "Hotel not found"));

           _mapper.Map(hotelDto,hotel);
            _hotelsRepo.Update(hotel);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "Hotel updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new ApiResponse(404, "Hotel not found"));

            _hotelsRepo.Delete(hotel);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "Hotel delete successfully"));
        }

    }
}
