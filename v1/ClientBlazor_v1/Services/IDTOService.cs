using ClientBlazor_v1.Models.DTO;

namespace ClientBlazor_v1.Services
{
    public interface IDTOService
    {
        Task<List<BuildingListElementDTO>> GetAllBuildingDTOsAsync();
        Task<List<RoomTypeDTO>> GetAllRoomTypeDTOsAsync();

        Task<RoomTypeDTO> PostRoomTypeFromDTOAsync(RoomTypeDTO dto);
        Task PutRoomTypeFromDTOAsync(int id, RoomTypeDTO dto);

        Task<List<RoomObjectRoomDTO>> GetAllRoomObjectRoomDTOsAsync();
    }
}
