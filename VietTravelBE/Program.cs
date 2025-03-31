using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Extensions;
using VietTravelBE.Helpers;
using VietTravelBE.Infrastructure;

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
        builder.Configuration.GetConnectionString("VietTravelConnStr"),
        //sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
        options => options.CommandTimeout(4500)
    );
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
var urlOrigins = new[]
{
    "http://localhost:5000",
    "https://localhost:5001"
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
