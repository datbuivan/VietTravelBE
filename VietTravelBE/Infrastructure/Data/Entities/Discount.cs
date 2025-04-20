using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("discount")]
    public class Discount: BaseEntity
    {
        public string Code { get; set; } // Mã giảm giá
        public double DiscountAmount { get; set; } // % giam gia
        public DateTime ExpiryDate { get; set; }
    }
}
