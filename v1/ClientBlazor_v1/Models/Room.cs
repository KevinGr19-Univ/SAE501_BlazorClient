using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Models
{
    public class Room
    {
        public string Name { get; set; }

        public IList<Equipment> Equipments { get; set; } = new List<Equipment>();
        public IList<Sensor> Sensors { get; set; } = new List<Sensor>();
        public IList<Actionnable> Actionnables { get; set; } = new List<Actionnable>();
    }
}
