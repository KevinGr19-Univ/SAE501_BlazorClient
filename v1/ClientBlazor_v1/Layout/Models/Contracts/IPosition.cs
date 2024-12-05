namespace ClientBlazor_v1.Models.Utils
{
    public interface IPosition : ITransform
    {
        double PosX { get; set; }
        double PosY { get; set; }
        double PosZ { get; set; }

        void CopyPositionFrom(IPosition other)
        {
            PosX = other.PosX;
            PosY = other.PosY;
            PosZ = other.PosZ;
        }
    }
}
