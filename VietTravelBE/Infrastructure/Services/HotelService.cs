using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Core.Specifications;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Extensions;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        private readonly IGenericRepository<Hotel> _hotelRepo;
        private readonly IHotelImageService _imageService;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepo;

        public HotelService(
            IGenericRepository<Hotel> hotelsRepo,
            IHotelImageService imageService,
            IUnitOfWork unit,
            IImageRepository imageRepo,
        IMapper mapper
        )
        {
            _hotelRepo = hotelsRepo;
            _imageService = imageService;
            _unit = unit;
            _imageRepo = imageRepo;
            _mapper = mapper;
        }
        //public async Task<IReadOnlyList<HotelDto>> GetHotelsWithParam(SpecParams? specParams)
        //{
        //    var spec = specParams == null
        //        ? new BaseSpecification<Hotel>()
        //        : new Specification<Hotel>(specParams); ;
        //    var hotels = await _hotelRepo.ListAsync(spec);
        //    var hotelDtos = _mapper.Map<IReadOnlyList<HotelDto>>(hotels);
        //    if (specParams?.Sort == "priceAsc")
        //    {
        //        hotelDtos = hotelDtos.OrderBy(dto => dto.Price).ToList();
        //    }
        //    else if (specParams?.Sort == "priceDesc")
        //    {
        //        hotelDtos = hotelDtos.OrderByDescending(dto => dto.Price).ToList();
        //    }
        //    else
        //    {
        //        hotelDtos = hotelDtos.OrderBy(dto => dto.Name).ToList(); // Sắp xếp mặc định theo Name
        //    }

        //    return hotelDtos;
        //}
        public async Task<IReadOnlyList<HotelDto>> GetHotels()
        {
            var hotels = await _hotelRepo.ListAllAsync();
            var result = new List<HotelDto>();
            foreach (var hotel in hotels)
            {
                var images = await _imageRepo
                    .GetImagesByEntityIdAsync(hotel.Id, ImageType.Hotel);
                result.Add(new HotelDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    PhoneNumber = hotel.PhoneNumber,
                    TitleIntroduct = hotel.TitleIntroduct,
                    ContentIntroduct = hotel.ContentIntroduct,
                    CityId = hotel.CityId,
                    Images = images?.Select(image => new ImageDto
                    {
                        Id = image.Id,
                        Url = image.Url,
                        IsPrimary = image.IsPrimary
                    }).ToList() ?? new List<ImageDto>(),
                });
            }
            return result;
        }

        public async Task<HotelDto> CreateHotel(HotelCreateDto hotelCreateDto)
        {
            if (hotelCreateDto == null)
                throw new ArgumentNullException(nameof(hotelCreateDto), "Data hotelCreateDto not null!");

            using (var transaction = await _unit.BeginTransactionAsync())
            {
                try
                {
                    var hotel = hotelCreateDto.CreateDtoToHotel();
             
                    _hotelRepo.Add(hotel);
                    await _unit.Complete();

                    if (hotelCreateDto.Images != null && hotelCreateDto.Images.Any())
                    {
                        var images = await _imageService.AddImagesAsync(hotel, hotelCreateDto.Images);
                        hotel.Images = images.Select(image => new Image
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
                    var hotelDto =  hotel.ToHotelDto();
                    return hotelDto;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while creating the hotel", ex);
                }
            }
        }

        public async Task<HotelDto> UpdateHotel(int id, HotelCreateDto hotelUpdateDto)
        {
            if (hotelUpdateDto == null)
                throw new ApiException(400, "Data invalid");

            await using var transaction = await _unit.BeginTransactionAsync();
            try
            {
                var hotel = await _hotelRepo.GetByIdAsync(id);

                if (hotel == null)
                    throw new KeyNotFoundException($"Hotel with ID {id} not found.");

                hotel.UpdateDtoToHotel(hotelUpdateDto);

                var images = await _imageRepo
                    .GetImagesByEntityIdAsync(hotel.Id, ImageType.Hotel);

                if(hotelUpdateDto.Images != null && hotelUpdateDto.Images.Any())
                {
                    if(images != null && images.Any())
                    {
                        foreach (var image in images)
                        {
                            if (!string.IsNullOrEmpty(image.PublicId))
                                await _imageService.DeleteImageAsync(image.PublicId);
                        }
                        _imageRepo.RemoveRange(images);
                    }

                    var newImages = await _imageService.AddImagesAsync(hotel, hotelUpdateDto.Images);
                    hotel.Images = newImages.Select(image => new Image
                    {
                        Id = image.Id,
                        Url = image.Url,
                        ImageType = image.ImageType,
                        EntityId = image.EntityId,
                        PublicId = image.PublicId,
                        IsPrimary = image.IsPrimary,
                    }).ToList();
                }

                _hotelRepo.Update(hotel);
                await _unit.Complete();

                await transaction.CommitAsync();
                var hotelDto = hotel.ToHotelDto();
                return hotelDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApiException(500, "An error occurred while updating the hotel", ex.Message);
            }
        }
    }
}
