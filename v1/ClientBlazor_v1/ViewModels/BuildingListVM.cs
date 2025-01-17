using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.DTO;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingListVM
    {
        private readonly IService<Building> _buildingService;
        private readonly IService<Room> _roomService;
        private readonly IDTOService _dtoService;

        public List<BuildingListElementDTO> BuildingDTOs { get; set; } = null;

        public BuildingListVM(IService<Building> buildingService, IService<Room> roomService, IDTOService dtoService)
        {
            _buildingService = buildingService;
            _roomService = roomService;
            _dtoService = dtoService;
        }

        public async Task LoadBuildings()
        {
            BuildingDTOs = null;
            BuildingDTOs = await _dtoService.GetAllBuildingDTOsAsync();
            BuildingDTOs.ForEach(b => b.Rooms.ForEach(r => r.BuildingDTO = b));
        }

        public async Task DeleteBuilding(BuildingListElementDTO buildingDto)
        {
            await _buildingService.DeleteAsync(buildingDto.Id);
            BuildingDTOs.Remove(buildingDto);
        }

        public async Task DeleteRoom(BuildingListRoomDTO roomDto)
        {
            await _roomService.DeleteAsync(roomDto.Id);
            roomDto.BuildingDTO.Rooms.Remove(roomDto);
        }
    }
}
