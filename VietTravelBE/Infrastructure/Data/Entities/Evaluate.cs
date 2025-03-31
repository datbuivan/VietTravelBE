using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("evaluate")]
    public class Evaluate : BaseEntity
    {
        [Required]
        public int Eva { get; set; } //Đánh giá Thành Phố/ Tour/ Khách sạn/ Nhà hàng theo thứ tự 1/2/3/4 - 0 là không đánh giá
        [Required]
        public string Content { get; set; }
        [Required]
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        [Required]
        public int? UserId { get; set; }
        [NotMapped]
        public User? User { get; set; }
    }
}
