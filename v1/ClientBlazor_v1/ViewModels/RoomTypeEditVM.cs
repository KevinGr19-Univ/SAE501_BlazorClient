using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.DTO;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeEditVM
    {
        private readonly IDTOService _dtoService;

        public RoomTypeDTO RoomTypeDTO { get; private set; }
        public bool IsNew { get; private set; }

        public RoomTypeEditVM(IDTOService dtoService)
        {
            _dtoService = dtoService;
            SetNewModel();
        }

        public void SetNewModel()
        {
            RoomTypeDTO = new();
            IsNew = true;
        }

        public void SetExistingModel(RoomTypeDTO roomTypeDto)
        {
            RoomTypeDTO = roomTypeDto;
            IsNew = false;
        }

        public async Task Save()
        {
            if (IsNew)
            {
                var roomType = await _dtoService.PostRoomTypeFromDTOAsync(RoomTypeDTO);
                SetExistingModel(roomType);
            }
            else await _dtoService.PutRoomTypeFromDTOAsync(RoomTypeDTO.Id, RoomTypeDTO);
        }
    }
}
