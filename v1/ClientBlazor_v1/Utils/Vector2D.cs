namespace ClientBlazor_v1.Utils
{
    public class Vector2D // Not a struct because otherwise cannot use @bind-value
    {
        public double x;
        public double y;

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double LengthSqr => x * x + y * y;
        public double Length => Math.Sqrt(LengthSqr);

        public void Normalize()
        {
            double length = Length;
            if (length <= 0)
            {
                x = 0;
                y = 0;
            }
            else
            {
                x /= Length;
                y /= Length;
            }
        }

        public static Vector2D operator +(Vector2D left, Vector2D right) => new(left.x + right.x, left.y + right.y);
        public static Vector2D operator -(Vector2D v) => new(-v.x, -v.y);
        public static Vector2D operator -(Vector2D left, Vector2D right) => left + (-right);
        public static Vector2D operator *(Vector2D v, double f) => new(v.x * f, v.y * f);
        public static Vector2D operator /(Vector2D v, double f) => new(v.x / f, v.y / f);

        public override bool Equals(object? obj) => obj is Vector2D v && x == v.x && y == v.y;

        public override string ToString()
        {
            return $"V[x={x} y={y}]";
        }
    }
}
