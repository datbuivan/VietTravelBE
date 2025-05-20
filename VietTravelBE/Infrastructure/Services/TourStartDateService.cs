using AutoMapper;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourStartDateService: ITourStartDateService
    {
        private readonly IGenericRepository<TourStartDate> _tourStartDateRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public TourStartDateService(IGenericRepository<TourStartDate> tourStartDateRepo, IMapper mapper, IUnitOfWork unit)
        {
            _tourStartDateRepo = tourStartDateRepo;
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<TourStartDateDto> CreateAsync(TourStartDateCreateDto dto)
        {
            if (dto.TourId <= 0)
                throw new ArgumentException("TourId không hợp lệ");

            if (dto.AvailableSlots <= 0)
                throw new ArgumentException("Số chỗ phải lớn hơn 0");

            if (dto.StartDate < DateTime.UtcNow)
                throw new ArgumentException("Ngày bắt đầu phải ở tương lai");

            var tour = await _unit.GenericRepository<Tour>().GetByIdAsync(dto.TourId);
            if (tour == null)
                throw new ArgumentException("Tour không tồn tại");

            var tourStartDate = new TourStartDate
            {
                TourId = dto.TourId,
                AvailableSlots = dto.AvailableSlots,
                StartDate = dto.StartDate,
            };
            
            _tourStartDateRepo.Add(tourStartDate);
            await _unit.Complete();

            return _mapper.Map<TourStartDateDto>(tourStartDate);
        }

        public async Task<List<TourStartDateDto>> CreateMultipleAsync(List<TourStartDateCreateDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                throw new ArgumentException("Không có dữ liệu ngày khởi hành để tạo.");

            var tourId = dtos.First().TourId;

            if (dtos.Any(x => x.TourId != tourId))
                throw new ArgumentException("Tất cả ngày khởi hành phải thuộc cùng một Tour.");

            var tour = await _unit.GenericRepository<Tour>().GetByIdAsync(tourId);
            if (tour == null)
                throw new ArgumentException("Tour không tồn tại.");

            foreach (var dto in dtos)
            {
                if (dto.AvailableSlots <= 0)
                    throw new ArgumentException("Số chỗ phải lớn hơn 0.");

                if (dto.StartDate < DateTime.UtcNow)
                    throw new ArgumentException("Ngày bắt đầu phải ở tương lai.");
            }
            var entities = dtos.Select(dto => new TourStartDate
            {
                TourId = dto.TourId,
                AvailableSlots = dto.AvailableSlots,
                StartDate = dto.StartDate.ToUniversalTime()
            }).ToList();

            _tourStartDateRepo.AddRange(entities);
            await _unit.Complete();
            return _mapper.Map<List<TourStartDateDto>>(entities);
        }

        public async Task<IReadOnlyList<TourStartDateDto>> GetByTourIdAsync(int tourId)
        {
            var spec = new TourStartDateByTourIdSpecification(tourId);
            var startDates = await _tourStartDateRepo.ListAsync(spec);
            return _mapper.Map<IReadOnlyList<TourStartDateDto>>(startDates);
        }
    }
}
