using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VietTravelBE.Infrastructure.Data.Entities.Custom;

namespace VietTravelBE.Dtos
{
    public class TourCreateDto
    {
        [StringLength(100)]
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Precision(18, 2)]
        public decimal ChildPrice { get; set; }
        [Precision(18, 2)]
        public decimal SingleRoomSurcharge { get; set; }
        public int CityId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
