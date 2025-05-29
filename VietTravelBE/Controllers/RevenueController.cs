using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService ;
        }

        [HttpGet("all-revenue")]
        public async Task<ActionResult<ApiResponse<List<RevenueReportDto>>>> GetAllRevenue()
        {
            try
            {
                var reports = await _revenueService.GetAllRevenue();
                return Ok(new ApiResponse<List<RevenueReportDto>>(200, "Success", reports));
            }
            catch (Exception ex)
            {
                // Log the exception (logging implementation omitted for brevity)
                return StatusCode(500, new ApiResponse<string>(500, $"An error occurred while retrieving revenue data" + ex.Message ));
            }
        }

        [HttpGet("revenue-report/{year}")]
        public async Task<ActionResult<ApiResponse<RevenueReportDto>>> GetRevenueByYear(int year)
        {
            try
            {
                var reports = await _revenueService.GetRevenueByYear(year);
                return Ok(new ApiResponse<RevenueReportDto>(200, "Success", reports));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving revenue data.");
            }
        }
    }
}
