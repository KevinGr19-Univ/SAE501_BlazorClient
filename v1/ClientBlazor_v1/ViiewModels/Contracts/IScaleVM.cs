using ClientBlazor_v1.Models.Utils;

namespace ClientBlazor_v1.ViewModels.Utils
{
    public interface IScaleVM : IJSObjectVM, IScale
    {
        double IScale.ScaleX
        {
            get => JSGet<double>("scaling.x");
            set => JSSet("scaling.x", value);
        }

        double IScale.ScaleY
        {
            get => JSGet<double>("scaling.y");
            set => JSSet("scaling.y", value);
        }

        double IScale.ScaleZ
        {
            get => JSGet<double>("scaling.z");
            set => JSSet("scaling.z", value);
        }
    }
}
