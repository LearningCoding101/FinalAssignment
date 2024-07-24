using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class BookingReservationRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingReservationRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookingReservation>> GetAll()
        {
            return await _context.BookingReservations.Include(c => c.Customer).ToListAsync();
        }

        public async Task<BookingReservation?> GetById(int bookingReservationId)
        {
            return await _context.BookingReservations.FindAsync(bookingReservationId);
        }

        public async Task<IEnumerable<BookingReservation>> GetReservationsByCustomerId(int customerId)
        {
            return await _context.BookingReservations
                           .Where(br => br.CustomerId == customerId)
                           .ToListAsync();
        }

        public async Task Add(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Add(bookingReservation);
            await _context.SaveChangesAsync();
        }

        public async Task Update(BookingReservation bookingReservation)
        {
            _context.Update(bookingReservation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int bookingReservationId)
        {
            var bookingReservation = await _context.BookingReservations.FindAsync(bookingReservationId);
            if (bookingReservation != null)
            {
                _context.BookingReservations.Remove(bookingReservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
