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
        }

        public CapteurInterop AddCapteur(Capteur capteur)
        {
            var capteurInterop = new CapteurInterop();
            var dotnetRef = DotNetObjectReference.Create(capteurInterop);

            capteurInterop.SetJSObj(JSObj.Invoke<IJSInProcessObjectReference>("addCapteur", dotnetRef));
            capteurInterop.Capteur = capteur;
            return capteurInterop;
        }
    }
}
