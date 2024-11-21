using ClientBlazor_v1.Models;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.Interop.Utils
{
    public abstract class TransformInterop : BaseInterop
    {
        private const string POSITION = "position";
        private const string ROTATION = "rotation";
        private const string SCALE = "scaling";

        public double PosX
        {
            get => JSGet<double>($"{POSITION}.x");
            set
            {
                JSSet($"{POSITION}.x", value);
                OnPropertyChanged();
            }
        }

        public double PosY
        {
            get => JSGet<double>($"{POSITION}.y");
            set
            {
                JSSet($"{POSITION}.y", value);
                OnPropertyChanged();
            }
        }

        public double PosZ
        {
            get => JSGet<double>($"{POSITION}.z");
            set
            {
                JSSet($"{POSITION}.z", value);
                OnPropertyChanged();
            }
        }

        public double RotX
        {
            get => JSGet<double>($"{ROTATION}.x");
            set
            {
                JSSet($"{ROTATION}.x", value);
                OnPropertyChanged();
            }
        }

        public double RotY
        {
            get => JSGet<double>($"{ROTATION}.y");
            set
            {
                JSSet($"{ROTATION}.y", value);
                OnPropertyChanged();
            }
        }

        public double RotZ
        {
            get => JSGet<double>($"{ROTATION}.z");
            set
            {
                JSSet($"{ROTATION}.z", value);
                OnPropertyChanged();
            }
        }

        public double ScaleX
        {
            get => JSGet<double>($"{SCALE}.x");
            set
            {
                JSSet($"{SCALE}.x", value);
                OnPropertyChanged();
            }
        }

        public double ScaleY
        {
            get => JSGet<double>($"{SCALE}.y");
            set
            {
                JSSet($"{SCALE}.y", value);
                OnPropertyChanged();
            }
        }

        public double ScaleZ
        {
            get => JSGet<double>($"{SCALE}.z");
            set
            {
                JSSet($"{SCALE}.z", value);
                OnPropertyChanged();
            }
        }
    }
}
