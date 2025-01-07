using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double NorthOrientation { get; set; }

        public double Height { get; set; }

        public List<Vector2D> Base { get; set; } = new();

        public int IdBuilding { get; set; }

        public virtual Building Building { get; set; } = null!;

        public int IdRoomType { get; set; }

        public RoomType RoomType { get; set; }

        public List<RoomObject> ObjectsOfRoom { get; set; } = new();
    }

    
}
