using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoomTypeRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public RoomTypeRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public List<RoomType> GetAll()
        {
            return _context.RoomTypes.ToList();
        }

        public RoomType? GetById(int roomTypeId)
        {
            return _context.RoomTypes.Find(roomTypeId);
        }

        public void Add(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void Update(RoomType roomType)
        {
            _context.Update(roomType);
            _context.SaveChanges();
        }

        public void Delete(int roomTypeId)
        {
            var roomType = _context.RoomTypes.Find(roomTypeId);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();
            }
        }
    }
}
