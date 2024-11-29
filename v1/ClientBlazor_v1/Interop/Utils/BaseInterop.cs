using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientBlazor_v1.Interop.Utils
{
    public abstract class BaseInterop : INotifyPropertyChanged
    {
        public IJSInProcessObjectReference JSObj { get; protected set; }
        public void SetJSObj(IJSInProcessObjectReference jsObj)
        {
            JSObj ??= jsObj;
        }

        protected T JSGet<T>(string prop) => JSObj.Invoke<T>("dotnetGet", prop);
        protected void JSSet(string prop, object? value) => JSObj.InvokeVoid("dotnetSet", prop, value);

        public event PropertyChangedEventHandler? PropertyChanged;

        [JSInvokable] public void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new(name));

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
