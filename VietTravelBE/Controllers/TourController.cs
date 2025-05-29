using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;
using VietTravelBE.RequestHelpers;
using Xunit.Sdk;
namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TourController : BaseApiController<Tour, TourCreateDto, TourDto>
    {
        private readonly ITourService _tourService;
        private readonly IFileValidationService _fileValidationService;
        public TourController(IGenericRepository<Tour> repo, IUnitOfWork unit, IMapper mapper, IFileValidationService fileValidationService, ITourService tourService)
            : base(repo, unit, mapper)
        {
            _tourService = tourService;
            _fileValidationService = fileValidationService;
        }

        [HttpGet]
        public override async Task<ActionResult<ApiResponse<IReadOnlyList<TourDto>>>> GetAll()
        {
            try
            {
                var tours = await _tourService.GetTours();
                if (tours == null || !tours.Any())
                {
                    return NotFound(new ApiResponse<string>(404, "No tours found"));
                }

                return Ok(new ApiResponse<IReadOnlyList<TourDto>>(200, "Success", tours));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, $"An error occurred while fetching tours: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ApiResponse<TourDto>>> GetById(int id)
        {
            try
            {
                var tour = await _tourService.GetById(id);
                if (tour == null)
                    return NotFound(new ApiResponse<TourDto>(404, "Not found"));
                return Ok(new ApiResponse<TourDto>(200, "Success", data: tour));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Internal Server Error", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            return await base.Delete(id);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<TourDto>>> Create([FromForm]TourCreateDto tourDto)
        {

            if (tourDto.Images != null && tourDto.Images.Any() )
            {
                string errorMessage;
                foreach(var image in tourDto.Images)
                {
                    if (!_fileValidationService.ValidateFile(image, out errorMessage))
                    {
                        return BadRequest(new ApiResponse<string>(400, errorMessage));
                    }
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<TourCreateDto>(400, "Invalid Tour data"));
            try
            {
                var createdTour = await _tourService.CreateTour(tourDto);
                return CreatedAtAction(nameof(GetAll), new { id = createdTour.Id }, new ApiResponse<TourDto>(201, data: createdTour));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<TourDto>>> Update(int id, [FromForm] TourCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(400, "Invalid data"));

            try
            {
                var updatedTour = await _tourService.UpdateTour(id, dto);
                return Ok(new ApiResponse<TourDto>(200, "Tour updated successfully", updatedTour));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, $"Internal Server Error: {ex.Message}"));
            }
        }

        [HttpGet("spec-params")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<TourDto>>>> GetWidthSpec([FromQuery] TourSpecParams tourParams)
        {
            var toursDto = await _tourService.GetToursWithSpec(tourParams);
            if (toursDto == null)
            {
                return BadRequest(new ApiResponse<string>(404, "Tour with filter not found"));
            }
            return Ok(new ApiResponse<IReadOnlyList<TourDto>>(200, "Success", toursDto));
        }

    }
}

