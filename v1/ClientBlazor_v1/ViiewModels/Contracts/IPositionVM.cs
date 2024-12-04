using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.ViewModels.Utils
{
    public interface IPositionVM : IJSObjectVM, IPosition
    {
        double IPosition.PosX
        {
            get => JSGet<double>("position.x");
            set => JSSet("position.x", value);
        }

        double IPosition.PosY
        {
            get => JSGet<double>("position.y");
            set => JSSet("position.y", value);
        }

        double IPosition.PosZ
        {
            get => JSGet<double>("position.z");
            set => JSSet("position.z", value);
        }
    }
}
