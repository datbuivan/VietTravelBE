
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
    public class CityController : BaseApiController
    {
        private readonly IGenericRepository<City> _citiesRepo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CityController(IUnitOfWork unit, IMapper mapper, IGenericRepository<City> citiesRepo)
        {
            _unit = unit;
            _mapper = mapper;
            _citiesRepo = citiesRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<City>>> Getcities()
        {
           
            try
            {
                if (_citiesRepo == null)
                {
                    return StatusCode(500, new ApiResponse(500, "Repository is not initialized"));
                }
                var cities = await _citiesRepo.ListAllAsync();
                if (cities == null || !cities.Any())
                    return NotFound(new ApiResponse(404, "No cities found"));
                var data = _mapper.Map<IReadOnlyList<City>, IReadOnlyList<CityDto>>(cities);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Internal Server Error: {ex.Message}"));
            }
        }

        [HttpGet("{id:int}")] // api/hotels/2
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _citiesRepo.GetByIdAsync(id);

            if (city == null) return NotFound(new ApiResponse(404, "Not found hotel data"));

            var data = _mapper.Map<City, CityDto>(city);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateCity([FromBody] CityDto cityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid city data"));

            var city = _mapper.Map<City>(cityDto);

            _citiesRepo.Add(city);
            await _unit.Complete();

            var createdCity = _mapper.Map<CityDto>(city);

            return CreatedAtAction(nameof(GetCity), new { id = createdCity.Id }, createdCity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDto>> UpdateCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
                return BadRequest(new ApiResponse(400, "City ID mismatch"));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid city data"));

            var hotel = await _citiesRepo.GetByIdAsync(id);

            if (hotel == null)
                return NotFound(new ApiResponse(404, "City not found"));

            _mapper.Map(cityDto, hotel);
            _citiesRepo.Update(hotel);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "City updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            var hotel = await _citiesRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new ApiResponse(404, "City not found"));

            _citiesRepo.Delete(hotel);
            await _unit.Complete();

            return Ok(new ApiResponse(200, "City delete successfully"));
        }

    }
}
