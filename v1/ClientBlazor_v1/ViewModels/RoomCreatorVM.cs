﻿using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;
using ClientBlazor_v1.Utils;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomCreatorVM
    {
        private readonly IAPIService _api;

        public bool IsLoaded { get; private set; } = false;

        public IEnumerable<Building> Buildings { get; private set; }
        public IEnumerable<RoomType> RoomTypes { get; private set; }
        public List<Vector2D> BasePoints { get; private set; }

        private int? _idRoom;
        public Room Room { get; private set; }

        public RoomCreatorVM(IAPIService api)
        {
            _api = api;
            BasePoints = new();
        }

        public async Task Load(int? idRoom)
        {
            IsLoaded = false;

            _idRoom = idRoom;
            await Task.WhenAll(
                Task.Run(async() => { Buildings = await _api.GetBuildingsAsync(); }),
                Task.Run(async() => { RoomTypes = await _api.GetRoomTypesAsync(); }),
                Task.Run(async() => { Room = _idRoom is null ? new Room() : await _api.GetRoomAsync((int)_idRoom); })
            );

            IsLoaded = true;
        }

        public async Task SaveRoom()
        {
            Room.Base = BasePoints.ToList();

            if(_idRoom is null) await _api.PostRoomAsync(Room);
            else await _api.PutRoomAsync((int)_idRoom, Room);
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

            Vector2D[] vertices = MathUtils.Bevel(point, left, right, vertexCount, radius, inside);

            BasePoints.InsertRange(index, vertices);
            BasePoints.Remove(point);
        }

        public void DeletePoint(Vector2D point)
        {
            BasePoints.Remove(point);
        }
    }
}
