using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.Layout.Models.RoomObjects
{
    public class Heater : ITransformRotY
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double RotY { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }
    }
}
