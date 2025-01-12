using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Plug : ConnectedObject
    {
        public override string GetRootName() => "Prise connectée";
    }
}
