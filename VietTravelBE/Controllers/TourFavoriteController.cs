using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourFavoriteController : ControllerBase
    {
        private readonly ITourFavoriteService _favoriteService;

        public TourFavoriteController(ITourFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<TourFavorite>>>> GetFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var favorites = await _favoriteService.GetFavoritesAsync(userId);
            return Ok(new ApiResponse<IReadOnlyList<TourFavorite>>(200, "Success", favorites));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TourFavorite>>> AddFavorite(int tourId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await _favoriteService.AddFavoriteAsync(userId, tourId);
                return Ok(new ApiResponse<string>(201, "Favorite created successfully"));
            }
            catch (DuplicateFavoriteException ex)
            {
                return BadRequest(new ApiResponse<TourFavorite>(400, ex.Message));
            }
        }

        

        [Authorize]
        [HttpPost("sync")]
        public async Task<ActionResult<ApiResponse<string>>> SyncFavorites([FromBody] List<TourFavoriteDto> favoritesDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _favoriteService.SyncFavoritesAsync(userId, favoritesDto);
            return Ok(new ApiResponse<string>(200, "Sync Tour Favorite Success"));
        }

        [Authorize]
        [HttpDelete("{tourId}")]
        public async Task<ActionResult<ApiResponse<string>>> RemoveFavorite(int tourId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await _favoriteService.RemoveFavoriteAsync(userId, tourId);
                return Ok(new ApiResponse<string>(200, "Delete Tour Favorite Success"));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse<string>(404, "Tour Favorite Not Found"));
            }
        }
    }
}
