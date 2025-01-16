using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du bâtiment est requis")]
        [StringLength(50, ErrorMessage = "Le nom du bâtiment ne doit pas dépasser 50 caratères")]
        public string Name { get; set; }

        public List<Room> Rooms { get; set; } = new();
    }
}
