using Microsoft.JSInterop;

namespace ClientBlazor_v1.Interop
{
    public class Sensor
    {
        public IJSInProcessObjectReference JsObj { get; init; }

        public double X
        {
            get => JsObj.Invoke<double>("getX");
            set => JsObj.InvokeVoid("setX", value);
        }

        public Sensor(IJSInProcessObjectReference jsObj)
        {
            JsObj = jsObj;
        }
    }
}
