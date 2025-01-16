using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Siren : ActionnableObject
    {
        public override string GetRootName() => "Sirène";

        public override string GetJSBuilderName() => "addSiren";
        public override RoomObjectVM ToVM() => new SirenVM() { Object = this };
    }
}
