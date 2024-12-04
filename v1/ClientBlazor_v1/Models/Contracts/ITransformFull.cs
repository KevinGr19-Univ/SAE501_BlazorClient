namespace ClientBlazor_v1.Models.Utils
{
    public interface ITransformFull : IPosition, IRotation, IScale
    {
        void CopyTransformFrom(ITransformFull other)
        {
            CopyPositionFrom(other);
            CopyRotationFrom(other);
            CopyScaleFrom(other);
        }
    }
}
