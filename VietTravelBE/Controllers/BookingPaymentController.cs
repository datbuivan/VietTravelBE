using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using Xunit.Sdk;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingPaymentController : ControllerBase
    {
        private readonly IBookingPaymentService _bookingPaymentService;

        public BookingPaymentController(IBookingPaymentService bookingPaymentService)
        {
            _bookingPaymentService = bookingPaymentService;
        }

        [HttpPost("create-payment")]
        public async Task<ActionResult<ApiResponse<BookingPaymentResponseDto>>> CreateBookingAndPay([FromBody] BookingAndPayCreateDto request)
        {
            try
            {
                var paymentUrl = await _bookingPaymentService.CreateBookingAndPayAsync(request);
                return Ok(new ApiResponse<BookingPaymentResponseDto>(200, " Success", paymentUrl));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("vnpay/callback")]
        public async Task<IActionResult> VnPayCallback()
        {
            var result = await _bookingPaymentService.ProcessCallbackAsync(Request.Query);
            if (result.Success)
            {
                var successUrl = "http://localhost:4200/success-payment";
                return Redirect(successUrl);
            }
            else
            {
                var errorUrl = "http://localhost:4200/error-payment";

                return Redirect(errorUrl);
            }
        }
    }
}
