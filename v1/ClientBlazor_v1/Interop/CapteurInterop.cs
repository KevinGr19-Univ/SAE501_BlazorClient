using ClientBlazor_v1.Interop.Utils;
using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Interop
{
    public class CapteurInterop : TransformInterop
    {
        private Capteur _capteur;
        private string _name;

        public void Reset()
        {
            Name = Capteur.Name;

            PosX = Capteur.Position.X;
            PosY = Capteur.Position.Y;
            PosZ = Capteur.Position.Z;

            RotX = Capteur.Rotation.X;
            RotY = Capteur.Rotation.Y;
            RotZ = Capteur.Rotation.Z;

            ScaleX = Capteur.Scale.X;
            ScaleY = Capteur.Scale.Y;
            ScaleZ = Capteur.Scale.Z;
        }

        public Capteur Capteur
        {
            get => _capteur;
            set
            {
                _capteur = value;
                Reset();
            }
        }

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
