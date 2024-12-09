using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Window : RoomObject, IPosition, ISize, IOrientation
    {
        public double Orientation { get; set; }

        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }

        public double SizeX { get; set; }
        public double SizeY { get; set; }
        public double SizeZ { get; set; }

        public override string GetName() => "Fenêtre";
    }
}
