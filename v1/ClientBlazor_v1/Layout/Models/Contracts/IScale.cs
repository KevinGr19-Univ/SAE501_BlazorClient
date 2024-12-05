namespace ClientBlazor_v1.Models.Utils
{
    public interface IScale : ITransform
    {
        double ScaleX { get; set; }
        double ScaleY { get; set; }
        double ScaleZ { get; set; }

        void CopyScaleFrom(IScale other)
        {
            ScaleX = other.ScaleX;
            ScaleY = other.ScaleY;
            ScaleZ = other.ScaleZ;
        }
    }
}
