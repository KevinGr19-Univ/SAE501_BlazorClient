using ClientBlazor_v1.Models.Utils;
using ClientBlazor_v1.ViewModels;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.Interop
{
    public class Scene : JSObjectVM
    {
        public event EventHandler<OnElementSelectedEventArgs> OnElementSelected;
        public class OnElementSelectedEventArgs : EventArgs
        {
            public ITransform Transform;
        }
    }
}
