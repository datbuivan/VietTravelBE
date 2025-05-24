using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Data.Migrations;

namespace VietTravelBE.Infrastructure.Services
{
    public class TourFavoriteService : ITourFavoriteService
    {
        private readonly ITourFavoriteRepository _favoriteRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public TourFavoriteService(ITourFavoriteRepository favoriteRepo, IMapper mapper, IUnitOfWork unit, IImageRepository imageRepo)
        {
            _favoriteRepo = favoriteRepo;
            _mapper = mapper;
            _unit = unit;
            _imageRepo = imageRepo;
        }

        public async Task<TourFavorite> AddFavoriteAsync(string userId, int tourId)
        {
            if (await _favoriteRepo.ExistsAsync(userId, tourId))
            {
                throw new DuplicateFavoriteException("Tour already in favorites");
            }
            var favorite = new TourFavorite
            {
                UserId = userId,
                TourId = tourId,
                CreatedAt = DateTime.UtcNow
            };

            _favoriteRepo.Add(favorite);
            await _unit.Complete();
            return favorite;
        }

        public async Task<List<TourFavorite>> GetFavoritesAsync(string userId)
        {
            var tourFavorites = await _favoriteRepo.GetByUserIdAsync(userId);
            if(tourFavorites == null)
            {
                throw new ApiException(404, "Tour favorite not found");
            }
            foreach(var tourFavorite in tourFavorites)
            {
                var image =await _imageRepo.GetPrimaryImageByEntityIdAsync(tourFavorite.TourId, ImageType.Tour);
                tourFavorite.Tour.Images = new List<Image>
                {
                   image
                };

            }
            return tourFavorites;
        }

        public async Task RemoveFavoriteAsync(string userId, int tourId)
        {
            var favorite = await _favoriteRepo.GetByUserIdAndTourIdAsync(userId, tourId);
            if (favorite == null)
            {
                throw new KeyNotFoundException("Favorite tour not found");
            }
            _favoriteRepo.Delete(favorite);
            await _unit.Complete();

        }


        public async Task SyncFavoritesAsync(string userId, List<TourFavoriteDto> favoritesDto)
        {
            var newFavorites = new List<TourFavorite>();
            foreach (var favoriteDto in favoritesDto)
            {
                if (!await _favoriteRepo.ExistsAsync(userId, favoriteDto.TourId))
                {
                    favoriteDto.UserId = userId;
                    var favorite =_mapper.Map<TourFavorite>(favoriteDto);    
                    favorite.CreatedAt = DateTime.UtcNow;
                    newFavorites.Add(favorite);
                }
            }
            if (newFavorites.Count > 0)
            {
                _favoriteRepo.AddRange(newFavorites);
            }
        }
    }
}
