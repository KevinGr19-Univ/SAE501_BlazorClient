using ClientBlazor_v1.ViewModels.JS;
using Microsoft.AspNetCore.Components;

namespace ClientBlazor_v1.Components.Utils
{
    public abstract class UIUpdatableComponent : ComponentBase, IDisposable
    {
        public abstract IUpdateUI Updatable { get; }

        protected override async Task OnInitializedAsync()
        {
            TryBind();
        }

        public void Dispose()
        {
            Unbind();
        }

        private void VM_OnRequireUIUpdate(object? sender, EventArgs e)
        {
            UpdateUI();
        }

        public void TryBind()
        {
            if(Updatable is not null)
                Updatable.OnRequireUIUpdate += VM_OnRequireUIUpdate;
        }

        public void Unbind()
        {
            if(Updatable is not null)
                Updatable.OnRequireUIUpdate -= VM_OnRequireUIUpdate;
        }

        public void UpdateUI()
        {
            InvokeAsync(StateHasChanged);
        }
        
    }
}
