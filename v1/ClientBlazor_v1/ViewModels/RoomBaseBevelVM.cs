using ClientBlazor_v1.Models;
using ClientBlazor_v1.Utils;
using System.Drawing;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomBaseBevelVM : RoomBaseVM
    {
        public RoomBaseVM BaseVM { get; set; }

        public Vector2D PointToBevel { get; set; }

        private int _vertexCount = 6;
        public int VertexCount { get => _vertexCount; set => _vertexCount = Math.Clamp(value, 2, 20); }

        private double _radius = 1;
        public double Radius { get => _radius; set => _radius = Math.Max(0, value); }

        public bool Inside { get; set; } = true;

        public void RecalculatePoints()
        {
            Points = BaseVM.Points.ToList();
            if (Points.Count < 3) return;

            int index = Points.IndexOf(PointToBevel);
            if (index == -1) return;

            Vector2D left = Points[MathUtils.ModPositive(index - 1, Points.Count)];
            Vector2D right = Points[MathUtils.ModPositive(index + 1, Points.Count)];

            Vector2D[] vertices = MathUtils.Bevel(PointToBevel, left, right, VertexCount, Radius, Inside);

            Points.InsertRange(index, vertices);
            Points.Remove(PointToBevel);
        }

        public void ApplyChanges()
        {
            BaseVM.Points = Points;
        }
    }
}
