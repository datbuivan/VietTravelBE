using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourService
    {
        private readonly IGenericRepository<Tour> _tourRepo;
        //private readonly ICloudinaryService _cloudinary;
        private readonly ITourImageService _tourImageService;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public TourService(
            IGenericRepository<Tour> tourRepo, IMapper mapper,
            IUnitOfWork unit,
            ITourImageService tourImageService
            )
        {
            _tourRepo = tourRepo;
            _mapper = mapper;
            _unit = unit;
            _tourImageService = tourImageService;
        }

        public async Task<TourDto> CreateTour(TourCreateDto tourCreateDto)
        {

            using (var transaction = await _unit.BeginTransactionAsync())
            {
                try
                {
                    var tour = _mapper.Map<Tour>(tourCreateDto);
                    

                    _tourRepo.Add(tour);
                    await _unit.Complete();

                    if (tourCreateDto.PrimaryImage != null)
                    {
                        await _tourImageService.AddPrimaryImageAsync(tour, tourCreateDto.PrimaryImage);
                    }

                    await transaction.CommitAsync();
                    return _mapper.Map<TourDto>(tour);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while creating the tour", ex);
                }
            }
        }

    }
}
