using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("payment")]
    public class Payment: BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string PaymentMethod { get; set; } // "Credit Card", "PayPal", "Bank Transfer"
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } // Pending, Completed, Failed
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
