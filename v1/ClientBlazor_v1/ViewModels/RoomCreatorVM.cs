using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomCreatorVM
    {
        private readonly IService<Room> _roomService;
        private readonly IService<Building> _buildingService;
        private readonly IService<RoomType> _roomTypeService;

        public bool IsLoaded { get; private set; } = false;

        public IEnumerable<Building> Buildings { get; private set; }
        public IEnumerable<RoomType> RoomTypes { get; private set; }
        public RoomBaseVM RoomBaseVM { get; private set; }

        private int? _idRoom;
        public Room Room { get; private set; }

        public RoomCreatorVM(IService<Room> roomService, IService<Building> buildingService, IService<RoomType> roomTypeService)
        {
            _roomService = roomService;
            _buildingService = buildingService;
            _roomTypeService = roomTypeService;
            RoomBaseVM = new();
        }

        public async Task Load(int? idRoom)
        {
            IsLoaded = false;

            _idRoom = idRoom;
            await Task.WhenAll(
                Task.Run(async() => { Buildings = await _buildingService.GetAllAsync(); }),
                Task.Run(async() => { RoomTypes = await _roomTypeService.GetAllAsync(); }),
                Task.Run(async() => { Room = _idRoom is null ? new Room() : await _roomService.GetByIdAsync((int)_idRoom); })
            );

            RoomBaseVM.Points = Room.Base;
            IsLoaded = true;
        }

        public async Task SaveRoom()
        {
            if(_idRoom is null) await _roomService.PostAsync(Room);
            else await _roomService.PutAsync((int)_idRoom, Room);
        }

        public async Task Delete()
        {
            if(_idRoom is not null) await _roomService.DeleteAsync((int)_idRoom);
        }
    }
}
