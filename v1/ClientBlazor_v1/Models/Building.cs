namespace ClientBlazor_v1.Models
{
    public class Building
    {
        public string Name { get; set; }
        public List<Room> Rooms { get; set; } = [];
    }
}
