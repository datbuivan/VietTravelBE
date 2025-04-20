using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;

namespace VietTravelBE.Controllers
{
    public partial class TourController
    {
        [HttpPost]
        public override async Task<ActionResult<ApiResponse<TourCreateDto>>> Create([FromBody] TourCreateDto tourDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<TourDto>(400, "Invalid Tour data"));

            try
            {
                var createdTour = await _tourService.CreateTour(tourDto);
                return CreatedAtAction(nameof(GetAll), new { id = createdTour.Id }, new ApiResponse<TourCreateDto>(201, data: createdTour));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

    }
}

