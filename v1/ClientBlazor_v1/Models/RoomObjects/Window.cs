using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Window : RoomObject, IPosition, IOrientation, ISize
    {
        public double PosX { get; set; }

        public double PosY { get; set; }

        public double PosZ { get; set; }

        public double Orientation { get; set; }

        public double SizeX { get; set; }

        public double SizeY { get; set; }

        public double SizeZ { get; set; }

        public override string GetRootName() => "Fenêtre";

        public override string GetJSBuilderName() => "addWindow";
        public override RoomObjectVM ToVM() => new WindowVM() { Object = this };
    }
}
