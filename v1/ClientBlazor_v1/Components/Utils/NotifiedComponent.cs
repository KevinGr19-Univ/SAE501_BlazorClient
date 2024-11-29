using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ClientBlazor_v1.Components.Utils
{
    public abstract class NotifiedComponent : ComponentBase, IDisposable
    {

        protected abstract INotifyPropertyChanged Notifier { get; }

        protected override async Task OnInitializedAsync()
        {
            Notifier.PropertyChanged += Notifier_PropertyChanged;
        }

        public void Dispose()
        {
            Notifier.PropertyChanged -= Notifier_PropertyChanged;
        }

        private async void Notifier_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            await InvokeAsync(StateHasChanged);
        }

    }
}
