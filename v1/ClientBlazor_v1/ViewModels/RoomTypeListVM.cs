using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeListVM
    {
        private readonly IService<RoomType> _roomTypeService;

        public RoomTypeListVM(IService<RoomType> roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public bool IsLoaded { get; private set; } = false;
        public List<RoomType> RoomTypes { get; private set; } = null;

        public async Task Load()
        {
            IsLoaded = false;

            RoomTypes = null;
            RoomTypes = (await _roomTypeService.GetAllAsync()).ToList();

            IsLoaded = true;
        }

        public async Task DeleteRoomType(RoomType roomType)
        {
            if (!RoomTypes.Contains(roomType)) return;
            if (roomType.Rooms.Count > 0)
                throw new Exception("Rooms are associated with this type");

            await _roomTypeService.DeleteAsync(roomType.Id);
            RoomTypes.Remove(roomType);
        }
    }
}
