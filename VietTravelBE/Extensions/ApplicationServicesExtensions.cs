using VietTravelBE.Core.Interface;
using VietTravelBE.Infrastructure.Services;
using VietTravelBE.Infrastructure;

namespace VietTravelBE.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ITourImageService, TourImageService>();
            services.AddScoped<IHotelImageService, HotelImageService>();
            services.AddScoped<ICityImageService, CityImageService>();

            services.AddScoped<ITourScheduleService, TourScheduleService>();
            services.AddScoped<ITourStartDateService, TourStartDateService>();

            services.AddScoped<RoomService>();
            services.AddScoped<ICityService,CityService>();
            services.AddScoped<HotelService>();
            services.AddScoped<TourService>();

            services.AddScoped<IFileValidationService, FileValidationService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
