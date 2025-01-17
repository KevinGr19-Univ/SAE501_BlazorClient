using ClientBlazor_v1.Models.RoomObjects;
using System.ComponentModel.DataAnnotations;

namespace ClientBlazor_v1.Models
{
    public class Room : ICloneable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la salle est requis")]
        [StringLength(50, ErrorMessage = "Le nom de la salle doit faire 50 caractères maximum")]
        public string Name { get; set; }

        [Range(0, 360, ErrorMessage = "L'orientation de la salle doit être entre 0 et 360 degrés")]
        public double NorthOrientation { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La hauteur de la salle doit être positive", MinimumIsExclusive = true)]
        public double Height { get; set; }

        [MinLength(3, ErrorMessage = "La base de la salle doit comprendre au moins 3 points")]
        public List<Vector2D> Base { get; set; } = new();

        [DeniedValues(0, ErrorMessage = "Le bâtiment est requis")]
        public int IdBuilding { get; set; }
        public virtual Building Building { get; set; } = null!;

        [DeniedValues(0, ErrorMessage = "Le type de salle est requis")]
        public int IdRoomType { get; set; }
        public RoomType RoomType { get; set; }

        public List<RoomObject> ObjectsOfRoom { get; set; } = new();

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    
}
