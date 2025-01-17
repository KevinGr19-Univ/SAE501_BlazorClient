using ClientBlazor_v1.Models.DTO;
using System.Text.Json;

namespace ClientBlazor_v1.Services
{
    public class DTOService : WSService, IDTOService
    {
        public DTOService(HttpClient client, JsonSerializerOptions jsonSettings, IConfiguration config) : base(client, jsonSettings, config) { }

        public Task<List<BuildingListElementDTO>> GetAllBuildingDTOsAsync() => _GetAllAsync<BuildingListElementDTO>("api/building/DTO");

        public Task<List<RoomObjectRoomDTO>> GetAllRoomObjectRoomDTOsAsync() => _GetAllAsync<RoomObjectRoomDTO>("api/roomObjects/DTO");

        public Task<List<RoomTypeDTO>> GetAllRoomTypeDTOsAsync() => _GetAllAsync<RoomTypeDTO>("api/roomType/DTO");

        public Task<RoomTypeDTO> PostRoomTypeFromDTOAsync(RoomTypeDTO dto) => _PostAsync("api/roomType/DTO", dto);

        public Task PutRoomTypeFromDTOAsync(int id, RoomTypeDTO dto) => _PutAsync("api/roomType/DTO", id, dto);
    }
}
