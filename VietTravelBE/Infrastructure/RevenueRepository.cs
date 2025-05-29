using Microsoft.EntityFrameworkCore;
using VietTravelBE.Core.Interface;
using VietTravelBE.Dtos;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly DataContext _context;

        public RevenueRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RawRevenueDto>> GetRawRevenueDataAsync()
        {
            //return await _context.Payments
            //    .Where(p => p.Status == "Completed")
            //    .Join(_context.Bookings,
            //        payment => payment.BookingId,
            //        booking => booking.Id,
            //        (payment, booking) => new { payment, booking })
            //    .GroupBy(x => new { x.payment.PaymentDate.Year, x.payment.PaymentDate.Month, x.booking.Type })
            //    .Select(g => new RawRevenueDto
            //    {
            //        Year = g.Key.Year,
            //        Month = g.Key.Month,
            //        BookingType = g.Key.Type,
            //        Amount = g.Sum(x => x.payment.Amount),
            //    })
            //    .OrderBy(r => r.Year)
            //    .ThenBy(r => r.Month)
            //    .ToListAsync();

            return await _context.Set<Payment>()
                   .Where(p => p.Status == "Completed")
                  .Join(_context.Set<Booking>(),
                        payment => payment.BookingId,
                        booking => booking.Id,
                        (payment, booking) => new RawRevenueDto
                        {
                        Year = payment.PaymentDate.Year,
                        Month = payment.PaymentDate.Month,
                        BookingType = booking.Type,
                        Amount = payment.Amount,
                        PaymentStatus = payment.Status
                        })
                        .ToListAsync();
        }
    }
}
