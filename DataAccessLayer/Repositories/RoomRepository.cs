using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public RoomRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public List<RoomInformation> GetAll()
        {
            return _context.RoomInformations.ToList();
        }

        public RoomInformation? GetById(int roomId)
        {
            return _context.RoomInformations.Find(roomId);
        }

        public void Add(RoomInformation room)
        {
            _context.RoomInformations.Add(room);
            _context.SaveChanges();
        }

        public void Update(RoomInformation room)
        {
            _context.Update(room);
            _context.SaveChanges();
        }

        public void Delete(int roomId)
        {
            var room = _context.RoomInformations.Find(roomId);
            if (room != null)
            {
                _context.RoomInformations.Remove(room);
                _context.SaveChanges();
            }
        }
    }
}
