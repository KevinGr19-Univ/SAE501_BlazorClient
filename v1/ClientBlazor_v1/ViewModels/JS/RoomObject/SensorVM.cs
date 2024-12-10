using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.Utils;

namespace ClientBlazor_v1.ViewModels.JS.RoomObject
{
    public class SensorVM : RoomObjectVM, IPosition, IRotation
    {
        private Sensor _sensor;
        public Sensor Sensor
        {
            get => _sensor;
            set
            {
                _sensor = value;
                RequireUIUpdate();
            }
        }

        public override Models.RoomObjects.RoomObject RoomObject => Sensor;

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

        public double RotX
        {
            get => RoomObjectVMUtils.GetRotX(this);
            set => RoomObjectVMUtils.SetRotX(this, value);
        }

        public double RotY
        {
            get => RoomObjectVMUtils.GetRotY(this);
            set => RoomObjectVMUtils.SetRotY(this, value);
        }

        public double RotZ
        {
            get => RoomObjectVMUtils.GetRotZ(this);
            set => RoomObjectVMUtils.SetRotZ(this, value);
        }
    }
}
