using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomRepository _roomRepository;
        private readonly RoomTypeRepository _roomTypeRepository;

        public RoomService(RoomRepository roomRepository, RoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task AddRoom(RoomInformation roomInformation)
        {
            await _roomRepository.Add(roomInformation);
        }

        public async Task DeleteRoom(int roomId)
        {
            await _roomRepository.Delete(roomId);
        }

        public async Task<IEnumerable<RoomInformation>> GetAllRooms()
        {
            return await _roomRepository.GetAll();
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await _roomTypeRepository.GetAll();
        }

        public async Task<RoomInformation?> GetRoom(int roomId)
        {
            return await _roomRepository.GetById(roomId);
        }

        public async Task<IEnumerable<RoomInformation>> SearchRooms(string keyword)
        {
            var rooms = await _roomRepository.GetAll();
            List<RoomInformation> results = [];

            foreach (var room in rooms)
            {
                RoomType? roomType = await _roomTypeRepository.GetById(room.RoomTypeId);
                if (!string.IsNullOrEmpty(room.RoomNumber) && room.RoomNumber.Contains(keyword) 
                    || !string.IsNullOrEmpty(room.RoomDetailDescription) && room.RoomDetailDescription.Contains(keyword))
                {
                    results.Add(room);
                }
            }
            return results;
        }

        public async Task UpdateRoom(RoomInformation roomInformation)
        {
            await _roomRepository.Update(roomInformation);
        }
    }
}
