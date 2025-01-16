using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du type de salle est requis")]
        [StringLength(30, ErrorMessage = "Le nom du type de salle ne doit pas dépasser 30 caractères")]
        public string Name { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}
