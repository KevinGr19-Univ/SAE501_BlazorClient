using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Lamp : ActionnableObject
    {
        public override string GetRootName() => "Lampe";

        public override string GetJSBuilderName() => "addLamp";
        public override RoomObjectVM ToVM() => new LampVM() { Object = this };
    }
}
