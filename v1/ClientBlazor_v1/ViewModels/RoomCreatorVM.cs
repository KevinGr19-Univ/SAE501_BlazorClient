using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;
using ClientBlazor_v1.Utils;
using System.Drawing;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomCreatorVM
    {
        private readonly IAPIService _api;

        public bool IsLoaded { get; private set; } = false;

        public IEnumerable<Building> Buildings { get; private set; }
        public Room Room { get; private set; }
        public List<Vector2D> BasePoints { get; private set; }

        public RoomCreatorVM(IAPIService api)
        {
            _api = api;
            Room = new();
            BasePoints = new();
        }

        public async Task Load()
        {
            IsLoaded = false;

            Buildings = await _api.GetBuildingsAsync();

            IsLoaded = true;
        }

        public void AddBasePoint()
        {
            BasePoints.Add(new(0, 0));
        }

        public void RoundCorner(Vector2D point, int vertexCount, double radius, bool inside = false)
        {
            if (BasePoints.Count < 3) return;
            
            int index = BasePoints.IndexOf(point);
            if (index == -1) return;

            Vector2D left = BasePoints[MathUtils.ModPositive(index - 1, BasePoints.Count)];
            Vector2D right = BasePoints[MathUtils.ModPositive(index + 1, BasePoints.Count)];

            Vector2D[] vertices = MathUtils.Bevel(point, left, right, vertexCount, radius, true);

            BasePoints.InsertRange(index, vertices);
            BasePoints.Remove(point);
        }

        public void DeletePoint(Vector2D point)
        {
            BasePoints.Remove(point);
        }
    }
}
