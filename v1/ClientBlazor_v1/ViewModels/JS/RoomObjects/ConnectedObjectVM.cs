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
        public double RotX { get => GetSizeX(); set => SetRotX(value); }
        public double RotY { get => GetSizeY(); set => SetRotY(value); }
        public double RotZ { get => GetSizeZ(); set => SetRotZ(value); }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            ((IPosition)Object).CopyPosFrom(this);
            ((IRotation)Object).CopyRotFrom(this);
        }
    }
}
