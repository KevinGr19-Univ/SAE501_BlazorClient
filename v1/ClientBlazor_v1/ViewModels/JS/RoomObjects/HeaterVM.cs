﻿using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjects
{
    public class HeaterVM : RoomObjectVM<Heater>, IPosition, ISize, IOrientation
    {
        public double Orientation { get => GetRotY(); set => SetRotY(value); }
        public double SizeX { get => GetSizeX(); set => SetSizeX(value); }
        public double SizeY { get => GetSizeY(); set => SetSizeY(value); }
        public double SizeZ { get => GetSizeZ(); set => SetSizeZ(value); }
        public double PosX { get => GetPosX(); set => SetPosX(value); }
        public double PosY { get => GetPosY(); set => SetPosY(value); }
        public double PosZ { get => GetPosZ(); set => SetPosZ(value); }
    }
}
