using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.ViewModels.Utils
{
    public interface IRotationYVM : IJSObjectVM, IRotationY
    {
        double IRotationY.RotY
        {
            get => JSGet<double>("rotation.y");
            set => JSSet("rotation.y", value);
        }
    }
}
