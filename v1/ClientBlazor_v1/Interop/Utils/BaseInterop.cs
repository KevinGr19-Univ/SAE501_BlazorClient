using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientBlazor_v1.Interop.Utils
{
    public abstract class BaseInterop : INotifyPropertyChanged
    {
        public IJSInProcessObjectReference JsObj { get; internal set; }

        protected T JSGet<T>(string prop) => JsObj.Invoke<T>("dotnetGet", prop);
        protected void JSSet(string prop, object? value) => JsObj.InvokeVoid("dotnetSet", prop, value);

        public event PropertyChangedEventHandler? PropertyChanged;

        [JSInvokable] public void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new(name));
    }
}
