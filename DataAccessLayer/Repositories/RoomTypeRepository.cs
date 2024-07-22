using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class RoomTypeRepository
    {
        private readonly FuminiHotelManagementContext _context;
        public RoomTypeRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        public async Task<RoomType?> GetById(int roomTypeId)
        {
            return await _context.RoomTypes.FindAsync(roomTypeId);
        }

        public async Task Add(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RoomType roomType)
        {
            _context.Update(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int roomTypeId)
        {
            var roomType = await _context.RoomTypes.FindAsync(roomTypeId);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
