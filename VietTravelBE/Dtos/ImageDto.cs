using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class ImageDto: BaseEntity
    {
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }
}
