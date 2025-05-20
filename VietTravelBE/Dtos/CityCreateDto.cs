using System.ComponentModel.DataAnnotations;

namespace VietTravelBE.Dtos
{
    public class CityCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        [StringLength(2000)]
        public string TitleIntroduct { get; set; }
        [Required]

        [StringLength(2000)]
        public string ContentIntroduct { get; set; }
        [Required]

        public int RegionId { get; set; }

        public IFormFile? PrimaryImage {  get; set; }
    }
}
