using ClientBlazor_v1.Models.DTO;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Services;
using static ClientBlazor_v1.Models.DTO.RoomTypeDTO;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomObjectListVM
    {
        private readonly IService<RoomObject> _roomObjectService;
        private readonly IDTOService _dtoService;

        public RoomObjectListVM(IService<RoomObject> roomObjectService, IDTOService dtoService)
        {
            _roomObjectService = roomObjectService;
            _dtoService = dtoService;
        }

        public List<RoomObjectRoomDTO> Rooms { get; set; }
        public bool IsLoaded { get; set; } = false;

        public async Task Load()
        {
            IsLoaded = false;
            Rooms = await _dtoService.GetAllRoomObjectRoomDTOsAsync();
            Rooms.ForEach(SortRooms);
            IsLoaded = true;
        }

        public void SortRooms(RoomObjectRoomDTO roomDto)
        {
            roomDto.RoomObjects = roomDto.RoomObjects.OrderBy(obj => obj.Id).ToList();
        }

        public async Task SwitchRoomOfRoomObject(RoomObject roomObject, int newIdRoom)
        {
            if (newIdRoom == 0) return;

            var newRoomObject = (RoomObject)roomObject.Clone();
            newRoomObject.IdRoom = newIdRoom;

            await _roomObjectService.PutAsync(newRoomObject.Id, newRoomObject);
            Rooms.First(dto => dto.Id == roomObject.IdRoom).RoomObjects.Remove(roomObject);

            roomObject.IdRoom = newIdRoom;
            var newRoomDto = Rooms.First(dto => dto.Id == roomObject.IdRoom);
            newRoomDto.RoomObjects.Add(roomObject);
            SortRooms(newRoomDto);
        }

        public async Task DeleteRoomObject(RoomObject roomObject)
        {
            await _roomObjectService.DeleteAsync(roomObject.Id);

            var dto = Rooms.FirstOrDefault(dto => dto.Id == roomObject.IdRoom);
            if(dto is not null) dto.RoomObjects.Remove(roomObject);
        }
    }
}
