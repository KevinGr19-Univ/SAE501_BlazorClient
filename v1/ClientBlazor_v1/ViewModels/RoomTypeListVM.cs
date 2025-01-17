using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.DTO;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomTypeListVM
    {
        private readonly IService<RoomType> _roomTypeService;
        private readonly IDTOService _dtoService;

        public RoomTypeListVM(IService<RoomType> roomTypeService, IDTOService dtoService)
        {
            _roomTypeService = roomTypeService;
            _dtoService = dtoService;
        }

        public bool IsLoaded { get; private set; } = false;
        public List<RoomTypeDTO> RoomTypeDTOs { get; private set; } = null;

        public async Task Load()
        {
            IsLoaded = false;

            RoomTypeDTOs = null;
            RoomTypeDTOs = await _dtoService.GetAllRoomTypeDTOsAsync();

            IsLoaded = true;
        }

        public async Task DeleteRoomType(RoomTypeDTO roomTypeDto)
        {
            if (!RoomTypeDTOs.Contains(roomTypeDto)) return;
            if (roomTypeDto.Rooms.Count > 0)
                throw new Exception("Rooms are associated with this type");

            await _roomTypeService.DeleteAsync(roomTypeDto.Id);
            RoomTypeDTOs.Remove(roomTypeDto);
        }
    }
}
