namespace ClientBlazor_v1.Models.RoomObjects
{
    public abstract class RoomObject
    {
        public int Id { get; set; }
        public string? CustomName { get; set; }
        public int IdRoom { get; set; }
        public Room Room { get; set; }

        abstract public string GetRootName();
        public string GetFullName() => GetRootName() + (string.IsNullOrEmpty(CustomName) ? "" : $" \"{CustomName}\"");
    }
}
