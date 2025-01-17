namespace ClientBlazor_v1.Models.DTO
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoomDTO> Rooms { get; set; }

        public class RoomDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string BuildingName { get; set; }
        }
    }
}
