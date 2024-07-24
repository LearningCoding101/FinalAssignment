using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public RoomRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoomInformation>> GetAll()
        {
            return await _context.RoomInformations.Include(t => t.RoomType).ToListAsync();
        }

        public async Task<RoomInformation?> GetById(int roomId)
        {
            return await _context.RoomInformations.FindAsync(roomId);
        }

        public async Task Add(RoomInformation room)
        {
            _context.RoomInformations.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RoomInformation room)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int roomId)
        {
            var room = await _context.RoomInformations.FindAsync(roomId);
            if (room != null)
            {
                _context.RoomInformations.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
