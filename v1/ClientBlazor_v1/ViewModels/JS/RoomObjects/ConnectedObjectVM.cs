using ClientBlazor_v1.Models.RoomObjects.ConnectedObjects;
using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjects
{
    public abstract class ConnectedObjectVM<TConnectedObject> : RoomObjectVM<TConnectedObject>, IPosition, IRotation
        where TConnectedObject : ConnectedObject
    {
        public double PosX { get => GetPosX(); set => SetPosX(value); }
        public double PosY { get => GetPosY(); set => SetPosY(value); }
        public double PosZ { get => GetPosZ(); set => SetPosZ(value); }
        public double RotX { get => GetRotX(); set => SetRotX(value); }
        public double RotY { get => GetRotY(); set => SetRotY(value); }
        public double RotZ { get => GetRotZ(); set => SetRotZ(value); }
    }
}
