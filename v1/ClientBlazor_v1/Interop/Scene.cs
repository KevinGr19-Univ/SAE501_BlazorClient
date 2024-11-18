using Microsoft.JSInterop;

namespace ClientBlazor_v1.Interop
{
    public class Scene
    {
        public IJSInProcessObjectReference JsObj;

        public Scene(IJSInProcessObjectReference jsObj)
        {
            JsObj = jsObj;
        }

        public async Task<Sensor> AddSensor()
        {
            var sensorJsObj = await JsObj.InvokeAsync<IJSInProcessObjectReference>("clickAddElement");
            return new Sensor(sensorJsObj);
        }
    }
}
