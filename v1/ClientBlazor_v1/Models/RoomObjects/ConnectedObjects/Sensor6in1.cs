using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Sensor6in1 : Sensor
    {
        public override string GetRootName() => "Capteur 6-en-1";

        public override string GetJSBuilderName() => "addSensor6in1";
        public override RoomObjectVM ToVM() => new Sensor6in1VM() { Object = this };
    }
}
