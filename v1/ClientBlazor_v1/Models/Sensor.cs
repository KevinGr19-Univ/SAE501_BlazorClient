using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.Models
{
    public class Sensor : ITransformFull
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double RotX { get; set; }
        public double RotY { get; set; }
        public double RotZ { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }
    }
}
