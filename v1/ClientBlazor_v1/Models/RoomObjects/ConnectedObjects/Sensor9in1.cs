using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Sensor9in1 : Sensor
    {
        public override string GetRootName() => "Capteur 9-en-1";
    }
}
