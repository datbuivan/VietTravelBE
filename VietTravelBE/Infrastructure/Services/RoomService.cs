using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VietTravelBE.Dtos;
using VietTravelBE.Errors;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure.Services
{
    public class RoomService
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public RoomService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<RoomDto>> GetRoomByHotelId(int hotelId)
        {
            try
            {
                var rooms = await _context.Rooms
                                      .Where(r => r.HotelId == hotelId)
                                      .ToListAsync();
                if(rooms == null || !rooms.Any())
                {
                    throw new Exception("No rooms found for the specified hotel.");
                }
                var data = _mapper.Map< IReadOnlyList<Room>, IReadOnlyList<RoomDto>>(rooms);
                return data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving rooms. Please try again later.", ex);
            }
        }
    }
}
