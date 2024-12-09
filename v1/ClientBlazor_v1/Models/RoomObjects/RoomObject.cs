using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public abstract class RoomObject
    {
        public Guid GUID { get; set; }
        public Room Room { get; set; }

        public abstract string GetName();
    }
}
