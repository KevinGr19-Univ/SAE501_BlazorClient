using ClientBlazor_v1.Models.RoomObjects;
using System.Threading.Tasks.Sources;

namespace ClientBlazor_v1.Models
{
    public struct Vector2D
    {
        public double x;
        public double y;

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Room
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }

        public Vector2D[] Base { get; set; } = [];
        public double Height { get; set; }
        public double Orientation { get; set; }

        public Building Building { get; set; }
        public List<RoomObject> Objects { get; set; } = [];
    }
}
