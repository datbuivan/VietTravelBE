using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VietTravelBE.Core.Interface;
using VietTravelBE.Extensions;
using VietTravelBE.Helpers;
using VietTravelBE.Infrastructure;
using VietTravelBE.Infrastructure.Data.Entities;
using VietTravelBE.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfiles()); });
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("VietTravelConnStr")!,
        //sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
        options => options.CommandTimeout(4500)
    );
});

//builder.Services.AddIdentity<AppUser, IdentityRole>()
//    .AddEntityFrameworkStores<DataContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//    };
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
//    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
//});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<HotelService>();
builder.Services.AddScoped<TourService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        policy.WithOrigins("http://localhost:4300").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
var urlOrigins = new[]
{
    "http://localhost:4200",
    "http://localhost:4300"
};
app.UseCors(opt =>
{
    opt.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins(urlOrigins);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataContext>();
//var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await DataContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
