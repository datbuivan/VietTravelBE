using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [Table("tourpackage")]
    public class TourPackage : BaseEntity
    {

        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public string? Description { get; set; }
        public int NumberOfAdult { get; set; }
        public int NumberOfChildren { get; set; }
        [Precision(18, 2)]
        public decimal BasePrice { get; set; }
        public float Discount { get; set; }
        [Precision(18, 2)]
        public decimal LastPrice { get; set; }
        public string? ListScheduleTourPackage { get; set; }
        [NotMapped]
        public List<ScheduleTourPackage>? ScheduleTourPackages { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        public int TimePackageId { get; set; }
        public TimePackage? TimePackage { get; set; }
    }
}
