using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Utils
{
    public static class MathUtils
    {
        public static int ModPositive(int value, int mod)
        {
            int res = value % mod;
            res = res < 0 ? mod + res : res;
            return res;
        }

        public static Vector2D[] Bevel(Vector2D point, Vector2D left, Vector2D right, int vertexCount, double maxRadius, bool inside)
        {
            Vector2D toLeft = left - point;
            Vector2D toRight = right - point;

            double theta = Math.Atan2(toLeft.X * toRight.Y - toLeft.Y * toRight.X, toLeft.X * toRight.X + toLeft.Y * toRight.Y);
            if (theta > Math.PI) (toLeft, toRight) = (toRight, toLeft);

            double radius = Math.Min(maxRadius, Math.Min(toLeft.Length, toRight.Length));
            toLeft.Normalize();
            toRight.Normalize();

            if (inside)
                return Bevel(point + (toLeft + toRight) * radius, point + toLeft * radius, point + toRight * radius, vertexCount, radius, false);

            double thetaLeft = Math.Atan2(toLeft.Y, toLeft.X);
            if (thetaLeft < 0) thetaLeft = Math.PI * 2 + thetaLeft;

            double thetaStep = theta / (vertexCount - 1);

            Vector2D[] vertices = new Vector2D[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                double i_theta = thetaLeft + thetaStep * i;
                vertices[i] = new(
                    point.X + Math.Cos(i_theta) * radius,
                    point.Y + Math.Sin(i_theta) * radius
                );
            }

            return vertices;
        }
    }
}
