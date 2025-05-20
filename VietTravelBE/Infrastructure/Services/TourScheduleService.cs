using AutoMapper;
using Humanizer;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourScheduleService : ITourScheduleService
    {
        private readonly IGenericRepository<TourSchedule> _tourScheduleRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public TourScheduleService(
        IGenericRepository<TourSchedule> tourScheduleRepo,
        IMapper mapper,
        IUnitOfWork unit)
        {
            _tourScheduleRepo = tourScheduleRepo;
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<TourScheduleDto> CreateAsync(TourScheduleCreateDto dto)
        {
            if (dto.TourId <= 0)
                throw new ArgumentException("TourId không hợp lệ");

            var tour = await _unit.GenericRepository<Tour>().GetByIdAsync(dto.TourId);

            if (tour == null)
            {
                throw new ArgumentException("Tour không tồn tại");
            }
            int existingCount = await _tourScheduleRepo.CountAsync(x => x.TourId == dto.TourId);
            var tourSchedule = new TourSchedule
            {
                TourId = dto.TourId,
                Title = dto.Title,
                Description = dto.Description,
                DayNumber = existingCount + 1,
            };

            _tourScheduleRepo.Add(tourSchedule);
            await _unit.Complete(); 

            return _mapper.Map<TourScheduleDto>(tourSchedule);
        }

        public async Task<IReadOnlyList<TourScheduleDto>> GetByTourIdAsync(int tourId)
        {
            var spec = new TourScheduleByTourIdSpecification(tourId);
            var schedules = await _tourScheduleRepo.ListAsync(spec);
            return _mapper.Map<IReadOnlyList<TourScheduleDto>>(schedules);
        }

        public async Task<List<TourScheduleDto>> CreateMultipleAsync(List<TourScheduleCreateDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                throw new ArgumentException("No schedules to create.");

            var tourId = dtos.First().TourId;
            int existingCount = await _tourScheduleRepo.CountAsync(x => x.TourId == tourId);

            var entities = dtos.Select((dto, index) => new TourSchedule
            {
                TourId = dto.TourId,
                Title = dto.Title,
                Description = dto.Description,
                DayNumber = existingCount+ index + 1,
            }).ToList();

            _tourScheduleRepo.AddRange(entities);
            await _unit.Complete();
            return _mapper.Map<List<TourScheduleDto>>(entities);
        }
    }
}
