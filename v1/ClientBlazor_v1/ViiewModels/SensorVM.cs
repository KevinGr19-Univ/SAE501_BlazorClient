using ClientBlazor_v1.Models;
using ClientBlazor_v1.ViewModels.Utils;

namespace ClientBlazor_v1.ViewModels
{
    public class SensorVM : JSObjectVM, ITransformFullVM
    {
        private Sensor _sensor;
        public Sensor Sensor
        {
            get => _sensor;
            set
            {
                _sensor = value;
                ((ITransformFullVM)this).CopyTransformFrom(_sensor);
                OnPropertyChanged();
            }
        }


    }
}
