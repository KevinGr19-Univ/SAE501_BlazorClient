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

            double theta = Math.Atan2(toLeft.x * toRight.y - toLeft.y * toRight.x, toLeft.x * toRight.x + toLeft.y * toRight.y);
            if (theta > Math.PI) (toLeft, toRight) = (toRight, toLeft);

            double radius = Math.Min(maxRadius, Math.Min(toLeft.Length, toRight.Length));
            toLeft.Normalize();
            toRight.Normalize();

            // TODO: Fix radius && inside

            if (inside)
                return Bevel(point + toLeft + toRight, point + toLeft, point + toRight, vertexCount, maxRadius, false);

            double thetaLeft = Math.Atan2(toLeft.y, toLeft.x);
            if (thetaLeft < 0) thetaLeft = Math.PI * 2 + thetaLeft;

            double thetaStep = theta / (vertexCount - 1);

            Vector2D[] vertices = new Vector2D[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                double i_theta = thetaLeft + thetaStep * i;
                vertices[i] = new(
                    point.x + Math.Cos(i_theta) * radius,
                    point.y + Math.Sin(i_theta) * radius
                );
            }

            return vertices;
        }
    }
}
