using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeListVM
    {
        private readonly IAPIService _api;

        public RoomTypeListVM(IAPIService api)
        {
            _api = api;
        }

        public bool IsLoaded { get; private set; } = false;
        public List<RoomType> RoomTypes { get; private set; } = null;

        public async Task Load()
        {
            IsLoaded = false;

            RoomTypes = null;
            RoomTypes = (await _api.GetRoomTypesAsync()).ToList();

            IsLoaded = true;
        }

        public async Task DeleteRoomType(RoomType roomType)
        {
            if (!RoomTypes.Contains(roomType)) return;
            if (roomType.Rooms.Count > 0)
                throw new Exception("Rooms are associated with this type");

            await _api.DeleteRoomTypeAsync(roomType.Id);
            RoomTypes.Remove(roomType);
        }
    }
}
