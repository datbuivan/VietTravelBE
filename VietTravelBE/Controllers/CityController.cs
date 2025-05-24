
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;


namespace VietTravelBE.Controllers
{
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

        [HttpGet]
        public override async Task<ActionResult<ApiResponse<IReadOnlyList<CityDto>>>> GetAll()
        {
            try
            {
                var cities = await _cityService.GetCities();
                if (cities == null || !cities.Any())
                {
                    return NotFound(new ApiResponse<IReadOnlyList<CityDto>>(404, "No cities found"));
                }

                return Ok(new ApiResponse<IReadOnlyList<CityDto>>(200, "Success", cities));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<List<CityDto>>(500, $"An error occurred while fetching cities: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ApiResponse<CityDto>>> GetById(int id)
        {
            try
            {
                var city = await _cityService.GetById(id);
                if(city == null)
                {
                    return NotFound(new ApiResponse<CityDto>(404, "City not found by Id"));
                }
                return Ok(new ApiResponse<CityDto>(200, "Success", city));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CityDto>(500, $"Internal Server Error: {ex.Message}"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<CityDto>>> Create([FromForm] CityCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CityDto>(400, "Invalid data"));

            try
            {
                var result = await _cityService.CreateCity(dto);
                return Ok(new ApiResponse<CityDto>(200, "City created successfully", result));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CityDto>(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<CityDto>>> Update(int id, [FromForm] CityCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<CityDto>(400, "Invalid data"));

            try
            {
                var updatedCity = await _cityService.UpdateCity(id, dto);
                return Ok(new ApiResponse<CityDto>(200, "City updated successfully", updatedCity));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CityDto>(500, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            return await base.Delete(id);
        }
        [HttpGet("byRegion/{regionId}")]
        public async Task<ActionResult<IReadOnlyList<CityDto>>> GetCitiesByRegion(int regionId)
        {
            try
            {
                var cities = await _cityService.GetCitiesByRegionAsync(regionId);
                if (cities == null)
                {
                    return NotFound(new ApiResponse<CityDto>(404, "City not found by regionId"));
                }
                //var data = _mapper.Map<IReadOnlyList<City>, IReadOnlyList<CityDto>>(cities);

                return Ok(new ApiResponse<IReadOnlyList<CityDto>>(200, data: cities));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CityDto>(500, ex.Message, null));
            }
        }
    }

}
