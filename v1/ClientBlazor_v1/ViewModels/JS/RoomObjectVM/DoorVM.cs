using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjectVM
{
    public class DoorVM : JSObjectVM, IPosition, ISize, IOrientation
    {
        private Door _door;
        public Door Door
        {
            get => _door;
            set
            {
                _door = value;
                RequireUIUpdate();
            }
        }

        public double Orientation
        {
            get => RoomObjectVMUtils.GetRotY(this);
            set => RoomObjectVMUtils.SetRotY(this, value);
        }

        public double SizeX
        {
            get => RoomObjectVMUtils.GetSizeX(this);
            set => RoomObjectVMUtils.SetSizeX(this, value);
        }

        public double SizeY
        {
            get => RoomObjectVMUtils.GetSizeY(this);
            set => RoomObjectVMUtils.SetSizeY(this, value);
        }

        public double SizeZ
        {
            get => RoomObjectVMUtils.GetSizeZ(this);
            set => RoomObjectVMUtils.SetSizeZ(this, value);
        }

        public double PosX
        {
            get => RoomObjectVMUtils.GetPosX(this);
            set => RoomObjectVMUtils.SetPosX(this, value);
        }

        public double PosY
        {
            get => RoomObjectVMUtils.GetPosY(this);
            set => RoomObjectVMUtils.SetPosY(this, value);
        }

        public double PosZ
        {
            get => RoomObjectVMUtils.GetPosZ(this);
            set => RoomObjectVMUtils.SetPosZ(this, value);
        }
    }
}
