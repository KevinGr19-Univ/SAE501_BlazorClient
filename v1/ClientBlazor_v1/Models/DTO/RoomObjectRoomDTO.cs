using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Models.DTO
{
    public class RoomObjectRoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BuildingName { get; set; }

        public List<RoomObject> RoomObjects { get; set; }
    }
}