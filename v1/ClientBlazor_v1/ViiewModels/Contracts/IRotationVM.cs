using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.ViewModels.Utils
{
    public interface IRotationVM : IRotationYVM, IRotation
    {
        double IRotation.RotX
        {
            get => JSGet<double>("rotation.x");
            set => JSSet("rotation.x", value);
        }

        double IRotation.RotZ
        {
            get => JSGet<double>("rotation.z");
            set => JSSet("rotation.z", value);
        }

        // Intentional rewrite
        double IRotationY.RotY
        {
            get => JSGet<double>("rotation.y");
            set => JSSet("rotation.y", value);
        }
    }
}
