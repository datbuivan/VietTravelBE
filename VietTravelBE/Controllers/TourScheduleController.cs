using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourScheduleController : ControllerBase
    {
        private readonly ITourScheduleService _tourScheduleService;

        public TourScheduleController(ITourScheduleService tourScheduleService)
        {
            _tourScheduleService = tourScheduleService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TourScheduleDto>>> Create([FromBody] TourScheduleCreateDto tourScheduleCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<TourScheduleCreateDto>(400, "Invalid TourSchedule data"));

            try
            {
                var createdSchedule = await _tourScheduleService.CreateAsync(tourScheduleCreateDto);
                return Ok(new ApiResponse<TourScheduleDto>(200, "Tour schedule created successfully", createdSchedule));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<TourScheduleDto>>>> GetByTourId(int tourId)
        {
            try
            {
                var schedules = await _tourScheduleService.GetByTourIdAsync(tourId);
                return Ok(new ApiResponse<IReadOnlyList<TourScheduleDto>>(200, "Schedules retrieved successfully", schedules));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
        }
    }
}
