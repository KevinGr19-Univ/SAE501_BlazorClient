using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Services
{
    public class DummyAPIService : IAPIService
    {
        private readonly Dictionary<string, Building> _buildings;
        private readonly Dictionary<Guid, Room> _rooms;
        private readonly Dictionary<Guid, RoomObject> _roomObjects;

        public DummyAPIService()
        {
            _buildings = new() {
                {
                    "IUT Annecy",
                    new()
                    {
                        Name = "IUT Annecy",
                        Rooms = [
                            new()
                            {
                                GUID = Guid.NewGuid(),
                                Name = "D360",
                                Orientation = 30,
                                Height = 3,
                                Objects = [
                                    new Door(){ GUID = Guid.NewGuid() },
                                    new Sensor(){ GUID = Guid.NewGuid() }
                                ]
                            },
                            new()
                            {
                                GUID = Guid.NewGuid(),
                                Name = "D351",
                                Height = 3,
                                Orientation = 210,
                                Objects = []
                            }
                        ]
                    }
                },

                {
                    "Tetras",
                    new()
                    {
                        Name = "Tetras",
                        Rooms = [
                            new()
                            {
                                GUID = Guid.NewGuid(),
                                Name = "E710",
                                Height = 5,
                                Orientation = 42.21,
                                Objects = []
                            }
                        ]
                    }
                }
            };
            _rooms = new();
            _roomObjects = new();

            foreach (var building in _buildings.Values)
            {
                foreach(var room in building.Rooms)
                {
                    _rooms.Add(room.GUID, room);

                    room.Building = building;
                    room.BuildingID = building.Name;

                    foreach(var roomObject in room.Objects)
                    {
                        _roomObjects.Add(roomObject.GUID, roomObject);

                        roomObject.Room = room;
                    }
                }
            }
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return _buildings.Values;
        }

        public async Task<Room?> GetRoomAsync(Guid guid)
        {
            return _rooms.GetValueOrDefault(guid);
        }

        public async Task<Room> PostRoomAsync(Room room)
        {
            room.GUID = Guid.NewGuid();
            room.Building = _buildings[room.BuildingID];

            room.Building.Rooms.Add(room);
            _rooms.Add(room.GUID, room);

            return room;
        }
    }
}
