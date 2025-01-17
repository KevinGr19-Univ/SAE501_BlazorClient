using BlazorBootstrap;

namespace ClientBlazor_v1.Utils
{
    public static class Extensions
    {
        public static void NewFromException(this ToastService toastService, Exception ex)
        {
            if (ex is HttpRequestException httpEx)
            {
                toastService.Notify(new(ToastType.Danger, IconName.Globe, "Erreur HTTP", httpEx.StatusCode is not null ? $"{(int)httpEx.StatusCode} : {httpEx.StatusCode}" : "An error has occured."));
            }
            else
            {
                toastService.Notify(new(ToastType.Danger, IconName.X, "Erreur", "An error has occured."));
            }
        }
    }
}
