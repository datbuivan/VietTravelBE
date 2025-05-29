namespace VietTravelBE.Core.Specifications
{
    public class TourSpecParams
    {
        //private const int MaxPageSize = 50;
        //public int PageIndex { get; set; } = 1;
        //private int _pageSize = 6;
        //public int PageSize
        //{
        //    get => _pageSize;
        //    set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        //}
        public int? CityId { get; set; }

        public string? Sort { get; set; }
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }

        public decimal? MinPrice { get; set; } 
        public decimal? MaxPrice { get; set; } 
        public DateTime? StartDate { get; set; }
    }
}
