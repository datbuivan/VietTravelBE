using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email claim not found in user claims.");

            var appUser = await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (appUser is null)
                throw new InvalidOperationException("User not found with given email.");

            return appUser;
        }
    }
}
