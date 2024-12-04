using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientBlazor_v1.ViewModels.Utils;

namespace ClientBlazor_v1.ViewModels
{
    public abstract class JSObjectVM : IJSObjectVM
    {
        private IJSInProcessObjectReference _jsObj;
        public IJSInProcessObjectReference JSObj
        {
            get => _jsObj;
            set
            {
                _jsObj = value;
                JSSet("dotnetRef", DotNetObjectReference.Create<object>(value)); // TODO: Faire attention à la disposition de la ref
                OnPropertyChanged();
            }
        }

        public T JSGet<T>(string propertyName)
        {
            return JSObj.Invoke<T>("dotnetGet", propertyName);
        }

        public void JSSet(string propertyName, object? value)
        {
            JSObj.InvokeVoid("dotnetSet", propertyName, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
