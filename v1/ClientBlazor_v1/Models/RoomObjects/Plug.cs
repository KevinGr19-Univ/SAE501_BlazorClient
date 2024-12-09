using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Plug : ActionnableObject, IPosition, IRotation
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }

        public double RotX { get; set; }
        public double RotY { get; set; }
        public double RotZ { get; set; }

        public override string GetName() => "Prise électrique";
    }
}
