using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BookingReservationRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingReservationRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public List<BookingReservation> GetAll()
        {
            return _context.BookingReservations.ToList();
        }

        public BookingReservation? GetById(int bookingReservationId)
        {
            return _context.BookingReservations.Find(bookingReservationId);
        }

        public List<BookingReservation> GetReservationsByCustomerId(int customerId)
        {
            return _context.BookingReservations
                           .Where(br => br.CustomerId == customerId)
                           .ToList();
        }

        public void Add(BookingReservation bookingReservation)
        {
            _context.BookingReservations.Add(bookingReservation);
            _context.SaveChanges();
        }

        public void Update(BookingReservation bookingReservation)
        {
            _context.Update(bookingReservation);
            _context.SaveChanges();
        }

        public void Delete(int bookingReservationId)
        {
            var bookingReservation = _context.BookingReservations.Find(bookingReservationId);
            if (bookingReservation != null)
            {
                _context.BookingReservations.Remove(bookingReservation);
                _context.SaveChanges();
            }
        }
    }
}
