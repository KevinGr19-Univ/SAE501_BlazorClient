namespace ClientBlazor_v1.Models.Utils
{
    public struct Vector3
    {
        public static Vector3 Zero => new Vector3(0, 0, 0);

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
