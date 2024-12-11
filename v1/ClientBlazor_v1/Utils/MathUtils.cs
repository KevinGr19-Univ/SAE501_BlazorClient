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
            double radius = Math.Min(maxRadius, Math.Min(toLeft.Length, toRight.Length));
            toLeft.Normalize();
            toRight.Normalize();

            double thetaLeft = Math.Atan2(toLeft.y, toLeft.x);
            if (thetaLeft < 0) thetaLeft = Math.PI * 2 + thetaLeft;
            double thetaStep = -Math.Acos(toLeft.x * toRight.x + toLeft.y * toRight.y) / (vertexCount - 1);

            Vector2D[] vertices = new Vector2D[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                double theta = thetaLeft + thetaStep * i;
                if (inside)
                {
                    vertices[vertexCount - i - 1] = new(
                        point.x + Math.Cos(theta + Math.PI) * radius + toLeft.x + toRight.x,
                        point.y + Math.Sin(theta + Math.PI) * radius + toLeft.y + toRight.y
                    );
                }
                else
                {
                    vertices[i] = new(
                        point.x + Math.Cos(theta) * radius,
                        point.y + Math.Sin(theta) * radius
                    );
                }
            }

            return vertices;
        }
    }
}
