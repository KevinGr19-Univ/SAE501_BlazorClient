using System.Numerics;

namespace ClientBlazor_v1.Models
{
    public class Salle
    {
        public string Name { get; set; }
        public Vector3 Dimensions { get; set; }
        public double Orientation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
