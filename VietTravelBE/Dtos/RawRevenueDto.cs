using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Dtos
{
    public class RawRevenueDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public BookingType BookingType { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
