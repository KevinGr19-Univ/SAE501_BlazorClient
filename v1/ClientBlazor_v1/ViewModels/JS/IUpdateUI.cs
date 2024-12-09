using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels.JS
{
    public interface IUpdateUI
    {
        event EventHandler OnRequireUIUpdate;
        [JSInvokable] void RequireUIUpdate();
    }
}
