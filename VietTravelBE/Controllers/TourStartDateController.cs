using AutoMapper;
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
    public class TourStartDateController : BaseApiController<TourStartDate, TourStartDateCreateDto, TourStartDateDto>
    {
        private readonly ITourStartDateService _service;
        public TourStartDateController(IGenericRepository<TourStartDate> repo, IUnitOfWork unit, IMapper mapper, ITourStartDateService service)
            : base(repo, unit, mapper)
        {
            _service = service;
        }

        [HttpPost("create-start-date")]
        public override async Task<ActionResult<ApiResponse<TourStartDateDto>>> Create([FromBody] TourStartDateCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<TourScheduleCreateDto>(400, "Invalid TourSchedule data"));

            try
            {
                var createdSchedule = await _service.CreateAsync(dto);
                return Ok(new ApiResponse<TourStartDateDto>(200, "Tour schedule created successfully", createdSchedule));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
        }

        [HttpGet("by-tourId/{tourId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<TourStartDateDto>>>> GetByTourId(int tourId)
        {
            try
            {
                var schedules = await _service.GetByTourIdAsync(tourId);
                return Ok(new ApiResponse<IReadOnlyList<TourStartDateDto>>(200, "Schedules retrieved successfully", schedules));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
        }
    }
}
