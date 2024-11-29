using ClientBlazor_v1.Models;
using Microsoft.JSInterop;
using System.Data.SqlTypes;

namespace ClientBlazor_v1.Interop.Utils
{
    public abstract class TransformInterop : BaseInterop
    {
        private const string POSITION = "position";
        private const string ROTATION = "rotation";
        private const string SCALE = "scaling";

        public const double RAD2DEG = 180 / Math.PI;
        public const double DEG2RAD = Math.PI / 180;

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
            get => JSGet<double>($"{ROTATION}.x") * RAD2DEG;
            set
            {
                JSSet($"{ROTATION}.x", value * DEG2RAD);
                OnPropertyChanged();
            }
        }

        public double RotY
        {
            get => JSGet<double>($"{ROTATION}.y") * RAD2DEG;
            set
            {
                JSSet($"{ROTATION}.y", value * DEG2RAD);
                OnPropertyChanged();
            }
        }

        public double RotZ
        {
            get => JSGet<double>($"{ROTATION}.z") * RAD2DEG;
            set
            {
                JSSet($"{ROTATION}.z", value * DEG2RAD);
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
