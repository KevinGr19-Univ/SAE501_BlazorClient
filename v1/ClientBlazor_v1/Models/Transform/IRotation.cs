namespace ClientBlazor_v1.Models.Transform
{
    public interface IRotation
    {
        public double RotX { get; set; }
        public double RotY { get; set; }
        public double RotZ { get; set; }

        public void CopyRotFrom(IRotation other)
        {
            RotX = other.RotX;
            RotY = other.RotY;
            RotZ = other.RotZ;
        }
    }
}
