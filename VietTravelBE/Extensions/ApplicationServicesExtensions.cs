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
            services.AddScoped<ITourFavoriteRepository, TourFavoriteRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IStartDateRepository, StartDateRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRevenueRepository, RevenueRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<ITourRepository, TourRepository>();


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
            services.AddScoped<IHotelService,HotelService>();
            services.AddScoped<ITourService,TourService>();

            services.AddScoped<IFileValidationService, FileValidationService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ITourFavoriteService, TourFavoriteService>();

            services.AddScoped<IRevenueService, RevenueService>();

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IBookingPaymentService, BookingPaymentService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
