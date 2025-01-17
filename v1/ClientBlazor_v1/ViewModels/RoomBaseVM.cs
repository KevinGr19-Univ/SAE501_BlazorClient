using ClientBlazor_v1.Models;
using ClientBlazor_v1.Utils;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomBaseVM
    {
        public List<Vector2D> Points { get; set; } = new();

        public void AddBasePoint()
        {
            Points.Add(new(0, 0));
        }

        public void DeletePoint(Vector2D point)
        {
            Points.Remove(point);
        }
    }
}
