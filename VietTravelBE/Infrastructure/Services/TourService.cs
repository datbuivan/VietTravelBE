using AutoMapper;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Extensions;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.RequestHelpers;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourService : ITourService
    {
        private readonly IGenericRepository<Tour> _tourRepo;
        private readonly ITourImageService _imageService;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepo;
        private readonly ICityRepository _cityRepo;
        private readonly IScheduleRepository _scheduleRepo;
        private readonly IStartDateRepository _dateRepo;

        public TourService(
            IGenericRepository<Tour> tourRepo, 
            IMapper mapper,
            IUnitOfWork unit,
            ITourImageService imageService,
            ICityRepository cityRepo,
            IImageRepository imageRepo,
            IScheduleRepository scheduleRepo,
            IStartDateRepository dateRepo
            )
        {
            _tourRepo = tourRepo;
            _mapper = mapper;
            _unit = unit;
            _imageService = imageService;
            _imageRepo = imageRepo;
            _cityRepo = cityRepo;
            _dateRepo = dateRepo;
            _scheduleRepo = scheduleRepo;
            _dateRepo = dateRepo;
        }

        public async Task<IReadOnlyList<TourDto>> GetTours()
        {
            var tours = await _tourRepo.ListAllAsync();
            var result = new List<TourDto>();
            foreach( var tour in tours ) 
            {
                var images = await _imageRepo
                    .GetImagesByEntityIdAsync(tour.Id, ImageType.Tour);
                result.Add(new TourDto
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Price = tour.Price,
                    ChildPrice = tour.ChildPrice,
                    SingleRoomSurcharge = tour.SingleRoomSurcharge,
                    CityId = tour.CityId,
                    Images = images?.Select (image => new ImageDto 
                    { 
                        Id = image.Id,
                        Url = image.Url,
                        IsPrimary = image.IsPrimary
                    }).ToList() ?? new List<ImageDto>(),
                });
            }
            return result;
        }

        public async Task<TourDto> GetById(int id)
        {
            var tour = await _tourRepo.GetByIdAsync(id);
            var images = await _imageRepo
                .GetImagesByEntityIdAsync(tour.Id, ImageType.Tour);
            var city = await _cityRepo.GetByIdAsync(tour.CityId);
            var schedules = await _scheduleRepo.GetScheduleByTourId(tour.Id);
            var startDates = await _dateRepo.GetStartDateByTourId(tour.Id);
            var result = new TourDto
            {
                Id = tour.Id,
                Name = tour.Name,
                Price = tour.Price,
                ChildPrice = tour.ChildPrice,
                SingleRoomSurcharge = tour.SingleRoomSurcharge,
                CityId = tour.CityId,
                CityName = city.Name,
                TourSchedules = schedules.Select(schedule => new TourScheduleDto
                {
                    Id =schedule.Id,
                    DayNumber = schedule.DayNumber,
                    Title = schedule.Title,
                    Description = schedule.Description,

                }).ToList() ?? new List<TourScheduleDto>(),
                TourStartDates = startDates.Select(startDate => new TourStartDateDto
                {
                    Id = startDate.Id,
                    AvailableSlots = startDate.AvailableSlots,
                    StartDate = startDate.StartDate,
                    TourId = startDate.TourId,
                }).ToList() ?? new List<TourStartDateDto>(),
                Images = images?.Select(image => new ImageDto
                {
                    Id = image.Id,
                    Url = image.Url,
                    IsPrimary = image.IsPrimary
                }).ToList() ?? new List<ImageDto>(),
            };
            return result;
        }

        public async Task<TourDto> CreateTour(TourCreateDto tourCreateDto)
        {

            using (var transaction = await _unit.BeginTransactionAsync())
            {
                try
                {
                    var tour = tourCreateDto.CreateDtoToTour();
                    var images = new List<Image>();

                    _tourRepo.Add(tour);
                    await _unit.Complete();

                    if (tourCreateDto.Images != null && tourCreateDto.Images.Any())
                    {
                        await _imageService.AddImagesAsync(tour, tourCreateDto.Images);
                        tour.Images = images.Select(image => new Image
                        {
                            Id = image.Id,
                            Url = image.Url,
                            ImageType = image.ImageType,
                            EntityId = image.EntityId,
                            PublicId = image.PublicId,
                            IsPrimary = image.IsPrimary,
                        }).ToList();
                    }

                    await transaction.CommitAsync();
                    var tourDto = tour.ToTourDto();
                    return tourDto;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while creating the tour", ex);
                }
            }
        }

        public async Task<TourDto> UpdateTour(int id, TourCreateDto tourUpdatedto)
        {
            await using var transaction = await _unit.BeginTransactionAsync();

            try
            {
                var tour = await _tourRepo.GetByIdAsync(id);
                if (tour == null)
                    throw new KeyNotFoundException($"Tour with ID {id} not found.");

                tour.UpdateDtoToTour(tourUpdatedto);

                var images = await _imageRepo
                    .GetImagesByEntityIdAsync(tour.Id, ImageType.Tour);

                if (tourUpdatedto.Images != null && tourUpdatedto.Images.Any())
                {
                    if (images != null && images.Any())
                    {
                        foreach (var image in images)
                        {
                            if (!string.IsNullOrEmpty(image.PublicId))
                                await _imageService.DeleteImageAsync(image.PublicId);
                        }
                        _imageRepo.RemoveRange(images);
                    }

                    var newImages = await _imageService.AddImagesAsync(tour, tourUpdatedto.Images);
                    tour.Images = newImages.Select(image => new Image
                    {
                        Id = image.Id,
                        Url = image.Url,
                        ImageType = image.ImageType,
                        EntityId = image.EntityId,
                        PublicId = image.PublicId,
                        IsPrimary = image.IsPrimary,
                    }).ToList();
                }

                _tourRepo.Update(tour);
                await _unit.Complete();
                await transaction.CommitAsync();

                var tourDto = tour.ToTourDto();
                return tourDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApiException(500, "An error occurred while updating the tour", ex.Message);
            }
        }

        public async Task<IReadOnlyList<TourDto>> GetToursWithSpec(TourSpecParams tourParams)
        {
            var spec = new TourWithCityAndStartDateSpecification(tourParams);

            //var countSpec = new TourWithFiltersForCountSpecificication(tourParams);

            //var totalItem = await _tourRepo.CountAsync(countSpec);

            var tours = await _tourRepo.ListAsync(spec);


            var toursDto = tours.Select(tour => tour.ToTourDto()).ToList();
            var result = new List<TourDto>();
            foreach (var tour in tours)
            {
                var images = await _imageRepo
                    .GetImagesByEntityIdAsync(tour.Id, ImageType.Tour);
                var city = await _cityRepo.GetByIdAsync(tour.CityId);
                var schedules = await _scheduleRepo.GetScheduleByTourId(tour.Id);
                var startDates = await _dateRepo.GetStartDateByTourId(tour.Id);
                result.Add(new TourDto
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Price = tour.Price,
                    ChildPrice = tour.ChildPrice,
                    SingleRoomSurcharge = tour.SingleRoomSurcharge,
                    CityId = tour.CityId,
                    CityName = city.Name,
                    TourSchedules = schedules.Select(schedule => new TourScheduleDto
                    {
                        Id = schedule.Id,
                        DayNumber = schedule.DayNumber,
                        Title = schedule.Title,
                        Description = schedule.Description,

                    }).ToList() ?? new List<TourScheduleDto>(),
                    TourStartDates = startDates.Select(startDate => new TourStartDateDto
                    {
                        Id = startDate.Id,
                        AvailableSlots = startDate.AvailableSlots,
                        StartDate = startDate.StartDate,
                        TourId = startDate.TourId,
                    }).ToList() ?? new List<TourStartDateDto>(),
                    Images = images?.Select(image => new ImageDto
                    {
                        Id = image.Id,
                        Url = image.Url,
                        IsPrimary = image.IsPrimary
                    }).ToList() ?? new List<ImageDto>(),
                });
            }
            return result;

            //return new Pagination<TourDto>(tourParams.PageIndex, tourParams.PageSize,totalItem, toursDto);

        }

    }
}
