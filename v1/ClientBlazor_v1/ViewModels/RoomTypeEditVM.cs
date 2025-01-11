using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeEditVM
    {
        private readonly IAPIService _api;

        public RoomType RoomType { get; private set; }
        public bool IsNew { get; private set; }

        public RoomTypeEditVM(IAPIService api)
        {
            _api = api;
            SetNewModel();
        }

        public void SetNewModel()
        {
            RoomType = new();
            IsNew = true;
        }

        public void SetExistingModel(RoomType roomType)
        {
            RoomType = roomType;
            IsNew = false;
        }

        public async Task Save()
        {
            if (IsNew)
            {
                var roomType = await _api.PostRoomTypeAsync(RoomType);
                SetExistingModel(roomType);
            }
            else await _api.PutRoomTypeAsync(RoomType.Id, RoomType);
        }
    }
}
