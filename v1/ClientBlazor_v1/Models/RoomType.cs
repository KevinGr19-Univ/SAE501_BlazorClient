using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}
