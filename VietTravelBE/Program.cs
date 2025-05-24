using AutoMapper;
using CloudinaryDotNet;
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
using VietTravelBE.Infrastructure.Identity;
using VietTravelBE.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();

var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfiles()); });
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("VietTravelConnStr")!,
        options => options.CommandTimeout(4500)
    );
});

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddHttpClient();

var app = builder.Build();

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
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await DataContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedRolesAsync(services);
    await AppIdentityDbContextSeed.SeedAdminAsync(services);

}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
