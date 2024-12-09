using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientBlazor_v1.ViewModels.JS
{
    public interface IJSObjectVM : IUpdateUI
    {
        IJSInProcessObjectReference JSObj { get; set; }

        T JSGet<T>(string property);
        void JSSet(string property, object? value);
    }
}
