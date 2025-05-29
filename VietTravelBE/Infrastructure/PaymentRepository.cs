using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly DataContext _context;
        public PaymentRepository(DataContext context): base(context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByBookingIdAsync(int id)
        {
            return await _context.Set<Payment>().FirstOrDefaultAsync(p => p.BookingId == id);
        }
    }
}
