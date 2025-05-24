using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public partial class HotelController : BaseApiWithSpecController<Hotel, HotelCreateDto, HotelDto>
    {
        private readonly IHotelService _hotelService;
        private readonly IFileValidationService _fileValidationService;
        public HotelController(IGenericRepository<Hotel> repo, IUnitOfWork unit, IMapper mapper, IHotelService hotelService, IFileValidationService fileValidationService)
            : base(repo, unit, mapper)
        {
            _hotelService = hotelService;
            _fileValidationService = fileValidationService;
        }

        [HttpGet]
        public override async Task<ActionResult<ApiResponse<IReadOnlyList<HotelDto>>>> GetAll()
        {
            try
            {
                var hotels = await _hotelService.GetHotels();
                if (hotels == null || !hotels.Any())
                {
                    return NotFound(new ApiResponse<string>(404, "No hotels found"));
                }

                return Ok(new ApiResponse<IReadOnlyList<HotelDto>>(200, "Success", hotels));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, $"An error occurred while fetching hotels: {ex.Message}"));
            }
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<HotelDto>>> Create([FromForm] HotelCreateDto hotelDto)
        {

            //if (hotelDto.Images != null)
            //{
            //    string errorMessage;
            //    if (!_fileValidationService.ValidateFile(hotelDto.Images, out errorMessage))
            //    {
            //        return BadRequest(new ApiResponse<string>(400, errorMessage));
            //    }
            //}

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<HotelCreateDto>(400, "Invalid Hotel data"));
            try
            {
                var createdHotel = await _hotelService.CreateHotel(hotelDto);
                return CreatedAtAction(nameof(GetAll), new { id = createdHotel.Id }, new ApiResponse<HotelDto>(201, data: createdHotel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<HotelDto>>> Update(int id, [FromForm] HotelCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(400, "Invalid data"));

            try
            {
                var updatedHotel = await _hotelService.UpdateHotel(id, dto);
                return Ok(new ApiResponse<HotelDto>(200, "Hotel updated successfully", updatedHotel));
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            return await base.Delete(id);
        }

        
    }

}
