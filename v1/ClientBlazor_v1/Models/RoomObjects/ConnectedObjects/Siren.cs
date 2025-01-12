using ClientBlazor_v1.Models.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public class Siren : ConnectedObject
    {
        public override string GetRootName() => "Sirène";
    }
}
