using ClientBlazor_v1.Interop.Utils;
using ClientBlazor_v1.Models;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.Interop
{
    public class Scene
    {
        private IJSInProcessObjectReference JSObj { get; init; }

        public Scene(IJSInProcessObjectReference jsObj)
        {
            JSObj = jsObj;
            JSObj.InvokeVoid("setDotnetRef", DotNetObjectReference.Create(this));
        }

        public CapteurInterop AddCapteur(Capteur capteur)
        {
            var capteurInterop = new CapteurInterop();
            var dotnetRef = DotNetObjectReference.Create(capteurInterop);

            capteurInterop.SetJSObj(JSObj.Invoke<IJSInProcessObjectReference>("addCapteur", dotnetRef));
            capteurInterop.Capteur = capteur;
            return capteurInterop;
        }

        [JSInvokable]
        public async Task ElementSelected(DotNetObjectReference<CapteurInterop> dotnetRef)
        {
            var value = dotnetRef.Value;
            OnElementSelected?.Invoke(this, new() { Element = value });
        }

        public event EventHandler<OnElementSelectedEventArgs> OnElementSelected;
        public class OnElementSelectedEventArgs : EventArgs
        {
            public BaseInterop Element { get; set; }
        }
    }
}
