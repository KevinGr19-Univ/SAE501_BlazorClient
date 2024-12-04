namespace ClientBlazor_v1.Models.Utils
{
    public interface IRotation : IRotationY, ITransform
    {
        double RotX { get; set; }
        double RotZ { get; set; }

        void CopyRotationFrom(IRotation other)
        {
            RotX = other.RotX;
            RotY = other.RotY;
            RotZ = other.RotZ;
        }
    }
}
