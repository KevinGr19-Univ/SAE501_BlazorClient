using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeEditVM
    {
        private readonly IService<RoomType> _roomTypeService;

        public RoomType RoomType { get; private set; }
        public bool IsNew { get; private set; }

        public RoomTypeEditVM(IService<RoomType> roomTypeService)
        {
            _roomTypeService = roomTypeService;
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
                var roomType = await _roomTypeService.PostAsync(RoomType);
                SetExistingModel(roomType);
            }
            else await _roomTypeService.PutAsync(RoomType.Id, RoomType);
        }
    }
}
