namespace ClientBlazor_v1.Models
{
    public class Building
    {
        public string Name { get; set; }
        public IList<Room> Rooms { get; set; }
    }
}
