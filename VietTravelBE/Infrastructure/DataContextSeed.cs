using System.Net;
using System.Text.Json;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context)
        {
            if (!context.Cities.Any())
            {
                var cityData = File.ReadAllText("./Infrastructure/Data/SeedData/City.json");
                var city = JsonSerializer.Deserialize<List<City>>(cityData);
                if (city != null)
                {
                    context.Cities.AddRange(city);
                }
            }
            //if (!context.Hotel.Any())
            //{
            //    var hotelData = File.ReadAllText("./Infrastructure/Data/SeedData/Hotel.json");
            //    var hotel = JsonSerializer.Deserialize<List<Hotel>>(hotelData);
            //    if (hotel != null)
            //    {
            //        context.Hotel.AddRange(hotel);
            //    }
            //}
            //if (!context.Room.Any())
            //{
            //    var roomData = File.ReadAllText("./Infrastructure/Data/SeedData/Room.json");
            //    var room = JsonSerializer.Deserialize<List<Room>>(roomData);
            //    if (room != null)
            //    {
            //        context.Room.AddRange(room);
            //    }
            //}
            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
