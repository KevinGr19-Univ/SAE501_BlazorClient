namespace ClientBlazor_v1.Models.Transform
{
    public interface IPosition
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }

        public void CopyPosFrom(IPosition other)
        {
            PosX = other.PosX;
            PosY = other.PosY;
            PosZ = other.PosZ;
        }
    }
}
