using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class SensorCO2 : Sensor
    {
        public override string GetRootName() => "Capteur CO2";

        public override string GetJSBuilderName() => "addSensorCO2";
        public override RoomObjectVM ToVM() => new SensorCO2VM() { Object = this };
    }
}
