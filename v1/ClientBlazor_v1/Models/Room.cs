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
        public string Name { get; set; }

        public Vector2D[] Base { get; set; } = [];
        public double Height { get; set; }
        public double Orientation { get; set; }

        public Building Building { get; set; }
        public List<RoomObject> Objects { get; set; } = [];

        public class RoomObjects
        {
            public IEnumerable<Sensor> Sensors { get; init; }
            public IEnumerable<ActionnableObject> Actionnables { get; init; }
            public IEnumerable<RoomObject> Equipments { get; init; }

            public static RoomObjects SortObjects(Room room)
            {
                var sensors = new List<Sensor>();
                var actionnables = new List<ActionnableObject>();
                var equipments = new List<RoomObject>();

                foreach(var roomObject in room.Objects)
                {
                    if (roomObject is Sensor sensor) sensors.Add(sensor);
                    else if (roomObject is ActionnableObject actionnable) actionnables.Add(actionnable);
                    else equipments.Add(roomObject);
                }

                return new()
                {
                    Sensors = sensors,
                    Actionnables = actionnables,
                    Equipments = equipments
                };
            }
        }
    }
}
