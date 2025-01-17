using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Plug : ActionnableObject
    {
        public override string GetRootName() => "Prise connectée";

        public override string GetJSBuilderName() => "addPlug";
        public override RoomObjectVM ToVM() => new PlugVM() { Object = this };
    }
}
