using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public abstract class ConnectedObject : RoomObject, IPosition, IRotation
    {
        public string CustomId;
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double RotX { get; set; }
        public double RotY { get; set; }
        public double RotZ { get; set; }
    }
}
