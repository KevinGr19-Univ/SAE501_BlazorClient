using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Utils;

namespace ClientBlazor_v1.Models
{
    public class Room
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }

        public Vector2D[] Base { get; set; } = [];
        public double Height { get; set; }
        public double Orientation { get; set; }

        public string BuildingID { get; set; }
        public Building Building { get; set; }

        public List<RoomObject> Objects { get; set; } = [];
    }
}
