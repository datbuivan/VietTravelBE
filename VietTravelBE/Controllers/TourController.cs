using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using Xunit.Sdk;
namespace VietTravelBE.Controllers
{
    public partial class TourController
    {
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<TourDto>>> Update(int id, [FromBody] TourCreateDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            return await base.Delete(id);
        }

        [HttpPost]  
        //[Authorize(Roles = "ADMIN")]
        public override async Task<ActionResult<ApiResponse<TourDto>>> Create([FromForm]TourCreateDto tourDto)
        {

            if (tourDto.PrimaryImage != null)
            {
                string errorMessage;
                if (!_fileValidationService.ValidateFile(tourDto.PrimaryImage, out errorMessage))
                {
                    return BadRequest(new ApiResponse<string>(400, errorMessage));
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

    }
}

