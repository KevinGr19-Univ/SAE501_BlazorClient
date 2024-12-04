using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientBlazor_v1.ViewModels.Utils
{
    public interface IJSObjectVM : INotifyPropertyChanged
    {
        IJSInProcessObjectReference JSObj { get; set; }
        [JSInvokable] void OnPropertyChanged([CallerMemberName] string? name = null);

        T JSGet<T>(string propertyName);
        void JSSet(string propertyName, object? value);
    }
}
