using AutoMapper;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourService
    {
        private readonly IGenericRepository<Tour> _tourRepo;
        private readonly IGenericRepository<TourStartDate> _tourStartDateRepo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public TourService(IGenericRepository<Tour> tourRepo, IMapper mapper, IUnitOfWork unit, IGenericRepository<TourStartDate> tourStartDateRepo)
        {
            _tourRepo = tourRepo;
            _mapper = mapper;
            _unit = unit;
            _tourStartDateRepo = tourStartDateRepo;
        }

        public async Task<TourCreateDto> CreateTour( TourCreateDto tourDto)
        {
            var tour = _mapper.Map<Tour>(tourDto);
            if (tourDto.TourStartDates != null && tourDto.TourStartDates.Any())
            {
                tour.TourStartDates = tourDto.TourStartDates.Select(dateDto => new TourStartDate
                {
                    StartDate = dateDto.StartDate,
                    AvailableSlots = dateDto.AvailableSlots
                }).ToList();
            }
            _tourRepo.Add(tour);
            await _unit.Complete();
            return _mapper.Map<TourCreateDto>(tour);
        }

    }
}
