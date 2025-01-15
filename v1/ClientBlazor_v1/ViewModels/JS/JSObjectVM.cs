using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels.JS
{
    public abstract class JSObjectVM : IJSObjectVM, IDisposable
    {
        private DotNetObjectReference<object>? _dotnetRef;

        protected JSObjectVM()
        {
            _dotnetRef = DotNetObjectReference.Create<object>(this);
        }

        private IJSInProcessObjectReference _jsObj;
        public IJSInProcessObjectReference JSObj
        {
            get => _jsObj;
            set
            {
                if (_jsObj is not null) JSSet("dotnetRef", null);
                _jsObj = value;
                JSSet("dotnetRef", _dotnetRef);
            }
        }

        public T JSGet<T>(string property) => JSObj.Invoke<T>("dotnetGet", property);
        public void JSSet(string property, object? value) => JSObj.InvokeVoid("dotnetSet", property, value);

        [JSInvokable] public void RequireUIUpdate() => OnRequireUIUpdate?.Invoke(this, EventArgs.Empty);
        public event EventHandler OnRequireUIUpdate;

        public void Dispose()
        {
            _dotnetRef?.Dispose();
            JSObj?.Dispose();
        }

    }
}
