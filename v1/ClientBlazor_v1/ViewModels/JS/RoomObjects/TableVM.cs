using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjects
{
    public class TableVM : RoomObjectVM<Table>, IPosition, ISize, IOrientation
    {
        public double Orientation { get => GetRotY(); set => SetRotY(value); }
        public double SizeX { get => GetSizeX(); set => SetSizeX(value); }
        public double SizeY { get => GetSizeY(); set => SetSizeY(value); }
        public double SizeZ { get => GetSizeZ(); set => SetSizeZ(value); }
        public double PosX { get => GetPosX(); set => SetPosX(value); }
        public double PosY { get => GetPosY(); set => SetPosY(value); }
        public double PosZ { get => GetPosZ(); set => SetPosZ(value); }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            ((IPosition)Object).CopyPosFrom(this);
            ((ISize)Object).CopySizeFrom(this);
            Object.Orientation = Orientation;
        }
    }
}
