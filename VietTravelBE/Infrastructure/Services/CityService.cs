using AutoMapper;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Migrations;

namespace VietTravelBE.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ICityImageService _imageService;
        public CityService(ICityRepository cityRepo, IUnitOfWork unit, IMapper mapper, ICityImageService imageService, IImageRepository imageRepo)
        {
            _cityRepo = cityRepo;
            _unit = unit;
            _mapper = mapper;
            _imageService = imageService;
            _imageRepo = imageRepo;

        }

        public async Task<IReadOnlyList<CityDto>> GetCities()
        {
            var cities = await _cityRepo.ListAllAsync();
            var result = new List<CityDto>();
            foreach (var city in cities)
            {
                var image = await _imageRepo
                    .GetPrimaryImageByEntityIdAsync(city.Id, ImageType.City);

                result.Add(new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    TitleIntroduct = city.TitleIntroduct,
                    ContentIntroduct = city.ContentIntroduct,
                    RegionId = city.RegionId,
                    Image = new ImageDto
                    {
                        Id = image.Id,
                        Url = image.Url,
                        IsPrimary = image.IsPrimary
                    }
                });
            }
            return result;

        }

        public async Task<CityDto> GetById(int id)
        {
            var city = await _cityRepo.GetByIdAsync(id);
            if (city == null)
            {
                throw new ApiException(404, "City not found");
            }

            var image = await _imageRepo.GetPrimaryImageByEntityIdAsync(id, ImageType.City);

            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                TitleIntroduct = city.TitleIntroduct,
                ContentIntroduct = city.ContentIntroduct,
                RegionId = city.RegionId,
                Image = new ImageDto
                {
                    Id = image.Id,
                    Url = image.Url,
                    IsPrimary = image.IsPrimary
                }
            };
        }

        public async Task<CityDto> CreateCity(CityCreateDto dto)
        {
            using (var transaction = await _unit.BeginTransactionAsync())
            {
                try
                {
                    var city = _mapper.Map<City>(dto);


                    _cityRepo.Add(city);
                    await _unit.Complete();

                    if (dto.PrimaryImage != null)
                    {
                       await _imageService.AddPrimaryImageAsync(city, dto.PrimaryImage);
                    }

                    await transaction.CommitAsync();
                    return _mapper.Map<CityDto>(city);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while creating the city", ex);
                }
            }
        }

        public async Task<CityDto> UpdateCity(int id, CityCreateDto dto)
        {
            await using var transaction = await _unit.BeginTransactionAsync();

            try
            {
                var city = await _cityRepo.GetByIdAsync(id);
                if (city == null)
                    throw new ApiException(404, "City not found");

                _mapper.Map(dto, city);
                _cityRepo.Update(city);
                await _unit.Complete();

                if (dto.PrimaryImage != null)
                {
                    await _imageService.ReplacePrimaryImageAsync(city, dto.PrimaryImage);
                }

                await transaction.CommitAsync();
                return _mapper.Map<CityDto>(city);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApiException(500,"An error occurred while updating the city", ex.Message);
            }
        }


        public async Task<IReadOnlyList<CityDto>> GetCitiesByRegionAsync(int regionId)
        {
            var cities = await _cityRepo.GetCitiesByRegionIdAsync(regionId);
            var result = new List<CityDto>();
            foreach (var city in cities)
            {
                var image = await _imageRepo
                    .GetPrimaryImageByEntityIdAsync(city.Id, ImageType.City);

                result.Add(new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    TitleIntroduct = city.TitleIntroduct,
                    ContentIntroduct = city.ContentIntroduct,
                    RegionId = city.RegionId,
                    Image = new ImageDto
                    {
                        Id = image.Id,
                        Url = image.Url,
                        IsPrimary = image.IsPrimary
                    }
                });
            }
            return result;
        }
    }
}
