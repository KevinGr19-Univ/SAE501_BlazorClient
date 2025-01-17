using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Sensor9in1 : Sensor
    {
        public override string GetRootName() => "Capteur 9-en-1";

        public override string GetJSBuilderName() => "addSensor9in1";
        public override RoomObjectVM ToVM() => new Sensor9in1VM() { Object = this };
    }
}
