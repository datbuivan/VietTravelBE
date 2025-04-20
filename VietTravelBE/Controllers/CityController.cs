
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;


namespace VietTravelBE.Controllers
{
    public partial class CityController
    {

        [HttpGet("byRegion/{regionId}")]
        public async Task<ActionResult<IReadOnlyList<City>>> GetCitiesByRegion(int regionId)
        {
            try
            {
                var cities = await _cityService.GetCitiesByRegionAsync(regionId);
                if (cities == null)
                {
                    return NotFound(new ApiResponse<CityDto>(404, "City not found by regionId"));
                }
                var data = _mapper.Map<IReadOnlyList<City>, IReadOnlyList<CityDto>>(cities);

                return Ok(new ApiResponse<IReadOnlyList<CityDto>>(200, data: data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }
    }

}
