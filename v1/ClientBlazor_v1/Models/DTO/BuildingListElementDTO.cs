using ClientBlazor_v1.Models.RoomObjects;
using System.Text.Json.Serialization;

namespace ClientBlazor_v1.Models.DTO
{
    public class BuildingListElementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BuildingListRoomDTO> Rooms { get; set; }
    }

    public class BuildingListRoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomTypeName { get; set; }
        public List<RoomObject> EmptyObjects { get; set; }

        [JsonIgnore]
        public BuildingListElementDTO BuildingDTO { get; set; }
    }
}
