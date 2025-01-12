using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class SensorCO2 : Sensor
    {
        public override string GetRootName() => "Capteur CO2";
    }
}
