using ClientBlazor_v1.Utils;

namespace ClientBlazor_v1.ViewModels.JS
{
    public class CompassVM : JSObjectVM
    {
        public double Orientation => -JSGet<double>("orientation") * MathUtils.RAD2DEG;
    }
}