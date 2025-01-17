using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public class CustomObject : RoomObject, IPosition, IRotation, ISize
    {
        public int Color { get; set; }

        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double RotX { get; set; }
        public double RotY { get; set; }
        public double RotZ { get; set; }
        public double SizeX { get; set; }
        public double SizeY { get; set; }
        public double SizeZ { get; set; }

        public override string GetRootName() => "Objet";

        public override string GetJSBuilderName() => "addCustomObject";
        public override RoomObjectVM ToVM() => new CustomObjectVM() { Object = this };
    }
}
