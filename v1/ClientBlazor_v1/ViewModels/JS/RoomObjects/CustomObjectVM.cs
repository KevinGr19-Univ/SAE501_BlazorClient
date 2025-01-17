using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;
using Microsoft.JSInterop;
using System.Globalization;
using YamlDotNet.Core.Tokens;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjects
{
    public class CustomObjectVM : RoomObjectVM<CustomObject>, IPosition, IRotation, ISize
    {
        public double PosX { get => GetPosX(); set => SetPosX(value); }
        public double PosY { get => GetPosY(); set => SetPosY(value); }
        public double PosZ { get => GetPosZ(); set => SetPosZ(value); }
        public double RotX { get => GetRotX(); set => SetRotX(value); }
        public double RotY { get => GetRotY(); set => SetRotY(value); }
        public double RotZ { get => GetRotZ(); set => SetRotZ(value); }
        public double SizeX { get => GetSizeX(); set => SetSizeX(value); }
        public double SizeY { get => GetSizeY(); set => SetSizeY(value); }
        public double SizeZ { get => GetSizeZ(); set => SetSizeZ(value); }

        public int Color
        {
            get => Object.Color;
            set
            {
                Object.Color = value;
                UpdateColorJS();
            }
        }

        public override void ApplyObjectToVM(bool ignoreNew)
        {
            base.ApplyObjectToVM(ignoreNew);
            UpdateColorJS();
        }

        private void UpdateColorJS()
        {
            JSObj.InvokeVoid("setCustomObjectColor", Object.Color);
        }
    }
}
