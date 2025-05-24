using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Dtos
{
    public class TourFavoriteDto
    {
        public string UserId { get; set; }
        public int TourId { get; set; }
    }
}
