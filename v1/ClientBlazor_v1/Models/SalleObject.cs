using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.Models
{
    public class SalleObject
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        public Vector3 Scale { get; set; } = Vector3.One;
    }
}
