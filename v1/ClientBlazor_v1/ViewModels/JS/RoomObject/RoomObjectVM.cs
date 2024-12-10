using ClientBlazor_v1.Models;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels.JS.RoomObject
{
    public abstract class RoomObjectVM : JSObjectVM
    {
        public abstract Models.RoomObjects.RoomObject RoomObject { get; }

        public event EventHandler OnSelect;
        public event EventHandler OnClose;

        public void Select()
        {
            JSObj.InvokeVoid("sceneSelect");
            OnSelect?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            JSObj.InvokeVoid("sceneUnselect");
            OnClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
