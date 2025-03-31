using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Infrastructure.Data.Entities
{
    [NotMapped]
    public class ScheduleTourPackage : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
    }
}
