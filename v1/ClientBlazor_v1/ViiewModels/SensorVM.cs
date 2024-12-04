using ClientBlazor_v1.Models;
using ClientBlazor_v1.ViewModels.Utils;

namespace ClientBlazor_v1.ViewModels
{
    public class SensorVM : JSObjectVM, ITransformFullVM
    {
        public ITransformFullVM Transform => (ITransformFullVM)this;

        private Sensor _sensor;
        public Sensor Sensor
        {
            get => _sensor;
            set
            {
                _sensor = value;
                Transform.CopyTransformFrom(_sensor);
                OnPropertyChanged();
            }
        }


    }
}
