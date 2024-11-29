using ClientBlazor_v1.Interop.Utils;
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

        private T AddObject<T>(Func<T> interopFactory, string jsFactoryMethodName) where T : BaseInterop
        {
            var interop = interopFactory();
            var dotnetRef = DotNetObjectReference.Create<object>(interop);

            interop.SetJSObj(JSObj.Invoke<IJSInProcessObjectReference>(jsFactoryMethodName, dotnetRef));
            return interop;
        }

        public CapteurInterop AddCapteur()
            => AddObject(() => new CapteurInterop(), "addCapteur");

        public PorteInterop AddPorte()
            => AddObject(() => new PorteInterop(), "addPorte");

        [JSInvokable]
        public async Task ElementSelected(DotNetObjectReference<object> dotnetRef)
        {
            var value = (BaseInterop)dotnetRef.Value;
            OnElementSelected?.Invoke(this, new() { Element = value });
        }

        public event EventHandler<OnElementSelectedEventArgs> OnElementSelected;
        public class OnElementSelectedEventArgs : EventArgs
        {
            public BaseInterop Element { get; set; }
        }
    }
}
