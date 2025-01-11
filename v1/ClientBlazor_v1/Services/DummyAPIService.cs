using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Services
{
    public class DummyAPIService : IAPIService
    {
        private readonly Dictionary<int, Building> _buildings;
        private readonly Dictionary<int, Room> _rooms;
        private readonly Dictionary<int, RoomType> _roomTypes;
        private readonly Dictionary<int, RoomObject> _roomObjects;

        private int _idBuilding, _idRoom, _idRoomType, _idRoomObject;

        public DummyAPIService()
        {
            _buildings = new()
            {

            };

            _roomTypes = new()
            {
                { 1, new() { Id = 1, Name = "TP" } },
                { 2, new() { Id = 2, Name = "Amphi" } },
            };

            _idBuilding = _buildings.Count > 0 ? _buildings.Values.Select(b => b.Id).Max() : 0;
            _idRoomType = _roomTypes.Count > 0 ? _roomTypes.Values.Select(t => t.Id).Max() : 0;

            _rooms = new();
            _roomObjects = new();

            foreach (var building in _buildings.Values)
            {
                foreach(var room in building.Rooms)
                {
                    room.Id = ++_idRoom;
                    _rooms.Add(room.Id, room);
                    room.RoomType = _roomTypes[room.IdRoomType];

                    room.Building = building;
                    room.IdBuilding = building.Id;

                    foreach(var roomObject in room.ObjectsOfRoom)
                    {
                        roomObject.Id = ++_idRoomObject;
                        _roomObjects.Add(roomObject.Id, roomObject);

                        roomObject.Room = room;
                    }
                }
            }
        }

        public async Task DeleteRoomTypeAsync(int idRoomType)
        {
            _roomTypes.Remove(idRoomType);
        }

        public async Task<Building?> GetBuildingAsync(int idBuilding)
        {
            return _buildings.GetValueOrDefault(idBuilding);
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return _buildings.Values;
        }

        public async Task<Room?> GetRoomAsync(int idRoom)
        {
            return _rooms.GetValueOrDefault(idRoom);
        }

        public async Task<RoomType?> GetRoomTypeAsync(int idRoomType)
        {
            return _roomTypes.GetValueOrDefault(idRoomType);
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypesAsync()
        {
            return _roomTypes.Values;
        }

        public async Task<Building> PostBuildingAsync(Building building)
        {
            building.Id = ++_idBuilding;
            _buildings.Add(building.Id, building);
            return building;
        }

        public async Task<Room> PostRoomAsync(Room room)
        {
            room.Id = ++_idRoom;
            room.Building = _buildings[room.IdBuilding];
            room.RoomType = _roomTypes[room.IdRoomType];

            room.Building.Rooms.Add(room);
            room.RoomType.Rooms.Add(room);
            _rooms.Add(room.Id, room);

            return room;
        }

        public async Task<RoomType> PostRoomTypeAsync(RoomType roomType)
        {
            roomType.Id = ++_idRoomType;
            _roomTypes.Add(roomType.Id, roomType);

            return roomType;
        }

        public async Task PutBuildingAsync(int idBuilding, Building building) {}

        public async Task PutRoomAsync(int idRoom, Room room) {}

        public async Task PutRoomTypeAsync(int idRoomType, RoomType roomType) {}
    }
}
