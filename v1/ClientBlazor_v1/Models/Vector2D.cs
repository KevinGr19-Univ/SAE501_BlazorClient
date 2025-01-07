namespace ClientBlazor_v1.Models
{
    public class Vector2D
    {
        private double x;
        private double y;

        //getter and setter in case we need to place verification of values etc
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2D() : this(0, 0) { }

        public double LengthSqr => X * X + Y * Y;
        public double Length => Math.Sqrt(LengthSqr);

        public void Normalize()
        {
            double length = Length;
            if (length <= 0)
            {
                X = 0;
                Y = 0;
            }
            else
            {
                X /= Length;
                Y /= Length;
            }
        }

        public static Vector2D operator +(Vector2D left, Vector2D right) => new(left.X + right.X, left.Y + right.Y);
        public static Vector2D operator -(Vector2D v) => new(-v.X, -v.Y);
        public static Vector2D operator -(Vector2D left, Vector2D right) => left + (-right);
        public static Vector2D operator *(Vector2D v, double f) => new(v.X * f, v.Y * f);
        public static Vector2D operator /(Vector2D v, double f) => new(v.X / f, v.Y / f);

        public override bool Equals(object? obj) => obj is Vector2D v && X == v.X && Y == v.Y;

        public override string ToString()
        {
            return $"V[x={X} y={Y}]";
        }
    }
}
