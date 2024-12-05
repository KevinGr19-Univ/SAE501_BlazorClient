namespace ClientBlazor_v1.Models.Utils
{
    public interface ITransformRotY : IPosition, IRotationY, IScale
    {
        void CopyTransformFrom(ITransformRotY other)
        {
            CopyPositionFrom(other);
            RotY = other.RotY;
            CopyScaleFrom(other);
        }
    }
}
