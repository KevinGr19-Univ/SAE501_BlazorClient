﻿using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Sensor9in1 : Sensor, IPosition, IRotation
    {
        public double PosX { get; set; }

        public double PosY { get; set; }

        public double PosZ { get; set; }

        public double RotX { get; set; }

        public double RotY { get; set; }

        public double RotZ { get; set; }

        public override string GetName() => "Capteur 9-en-1";
    }
}
