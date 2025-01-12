namespace ClientBlazor_v1.Models.Transform
{
    public interface ISize
    {
        public double SizeX { get; set; }
        public double SizeY { get; set; }
        public double SizeZ { get; set; }

        public void CopySizeFrom(ISize other)
        {
            SizeX = other.SizeX;
            SizeY = other.SizeY;
            SizeZ = other.SizeZ;
        }
    }
}
