using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookingDetailRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingDetailRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookingDetail>> GetAll()
        {
            return await _context.BookingDetails.ToListAsync();
        }

        public async Task<BookingDetail?> GetById(int id)
        {
            return await _context.BookingDetails.FindAsync(id);
        }

        public async Task Add(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
        }

        public async Task Update(BookingDetail bookingDetail)
        {
            _context.Update(bookingDetail);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int bookingReservationId)
        {
            var bookingDetails = await _context.BookingDetails.Where(bd => bd.BookingReservationId == bookingReservationId).ToListAsync();
            if (bookingDetails.Any())
            {
                _context.BookingDetails.RemoveRange(bookingDetails);
                await _context.SaveChangesAsync();
            }
        }
    }
}
