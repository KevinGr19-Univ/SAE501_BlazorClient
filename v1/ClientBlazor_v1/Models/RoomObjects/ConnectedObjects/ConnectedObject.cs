
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBlazor_v1.Models.RoomObjects.ConnectedObjects
{
    public abstract class ConnectedObject : RoomObject
    {
        public string CustomId;
    }
}
