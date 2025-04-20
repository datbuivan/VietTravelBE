using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public TokenService(UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["JWT:ExpiryInMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<(string accessToken, string refreshToken)> RefreshToken(string expiredToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(expiredToken);
            var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                throw new SecurityTokenException("Invalid token");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new SecurityTokenException("Invalid refresh token or refresh token expired");

            var newAccessToken = await CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return (newAccessToken, newRefreshToken);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Quan trọng để có thể đọc token đã hết hạn
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
