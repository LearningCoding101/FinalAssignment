using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomInformation>> GetAllRooms();

        Task<RoomInformation?> GetRoom(int roomId);

        Task UpdateRoom(RoomInformation roomInformation);

        Task DeleteRoom(int roomId);

        Task AddRoom(RoomInformation roomInformation);

        Task<IEnumerable<RoomInformation>> SearchRooms(string keyword);

        Task<IEnumerable<RoomType>> GetAllRoomTypes();
    }
}
