using ClientBlazor_v1.Interop.Utils;
using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Interop
{
    public class CapteurInterop : TransformInterop
    {
        private string _name = "Capteur";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}
