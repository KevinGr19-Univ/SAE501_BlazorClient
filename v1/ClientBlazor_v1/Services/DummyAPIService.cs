using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Services
{
    public class DummyAPIService : IAPIService
    {
        private readonly Dictionary<int, Building> _buildings;
        private readonly Dictionary<int, Room> _rooms;
        private readonly Dictionary<int, RoomObject> _roomObjects;

        private int _idBuilding, _idRoom, _idRoomObject;

        public DummyAPIService()
        {
            _buildings = new() {
            };
            _rooms = new();
            _roomObjects = new();

            foreach (var building in _buildings.Values)
            {
                foreach(var room in building.Rooms)
                {
                    _rooms.Add(room.Id, room);

                    room.Building = building;
                    room.IdBuilding = building.Id;

                    foreach(var roomObject in room.ObjectsOfRoom)
                    {
                        _roomObjects.Add(roomObject.Id, roomObject);

                        roomObject.Room = room;
                    }
                }
            }
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

            room.Building.Rooms.Add(room);
            _rooms.Add(room.Id, room);

            return room;
        }

        public async Task<Building> PutBuildingAsync(int idBuilding, Building building)
        {
            _buildings.Remove(idBuilding);
            return await PostBuildingAsync(building);
        }

        public async Task<Room> PutRoomAsync(int idRoom, Room room)
        {
            _rooms.Remove(idRoom);
            return await PostRoomAsync(room);
        }
    }
}
