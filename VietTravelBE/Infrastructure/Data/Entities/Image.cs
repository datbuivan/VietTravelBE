using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("image")]
    public class Image: BaseEntity
    {
        public string Url { get; set; }
        public int EntityId { get; set; }
        public ImageType ImageType { get; set; }
    }
    public enum ImageType
    {
        Tour,
        Hotel
    }
}
