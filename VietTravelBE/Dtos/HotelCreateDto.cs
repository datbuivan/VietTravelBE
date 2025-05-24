using System.ComponentModel.DataAnnotations;

namespace VietTravelBE.Dtos
{
    public class HotelCreateDto
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(2000)]
        public string TitleIntroduct { get; set; }
        [StringLength(2000)]
        public string ContentIntroduct { get; set; }
        public int CityId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
