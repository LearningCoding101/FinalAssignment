using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BookingDetailRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingDetailRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public List<BookingDetail> GetAll()
        {
            return _context.BookingDetails.ToList();
        }

        public BookingDetail? GetById(int id)
        {
            return _context.BookingDetails.Find(id);
        }

        public void Add(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            _context.SaveChanges();
        }

        public void Update(BookingDetail bookingDetail)
        {
            _context.Update(bookingDetail);
            _context.SaveChanges();
        }

        public void Delete(int bookingReservationId)
        {
            var bookingDetails = _context.BookingDetails.Where(bd => bd.BookingReservationId == bookingReservationId).ToList();
            if (bookingDetails.Any())
            {
                _context.BookingDetails.RemoveRange(bookingDetails);
                _context.SaveChanges();
            }
        }
    }
}
