using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Sensor6in1 : Sensor
    {
        public override string GetRootName() => "Capteur 6-en-1";
    }
}
