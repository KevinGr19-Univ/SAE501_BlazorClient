using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.Utils;
using ClientBlazor_v1.ViewModels;
using ClientBlazor_v1.ViewModels.Utils;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels
{
    public class Scene : JSObjectVM
    {
        public SensorVM CreateSensor()
        {
            SensorVM sensorVM = new();
            sensorVM.JSObj = JSObj.Invoke<IJSInProcessObjectReference>("addSensor");
            sensorVM.Sensor = new Sensor() { Name = "Test" } ;
            return sensorVM;
        }

        public event EventHandler<OnElementSelectedEventArgs> OnElementSelected;
        public class OnElementSelectedEventArgs : EventArgs
        {
            public IJSObjectVM SceneObjectVM;
        }

        [JSInvokable]
        public async Task ElementSelected(DotNetObjectReference<object> sceneObjectVMRef)
        {
            OnElementSelected?.Invoke(this, new()
            {
                SceneObjectVM = (IJSObjectVM)sceneObjectVMRef.Value
            });
        }
    }
}
