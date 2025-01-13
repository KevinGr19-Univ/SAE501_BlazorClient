using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingListVM
    {
        private readonly IService<Building> _buildingService;
        private readonly IService<Room> _roomService;
        public List<Building> Buildings { get; set; } = null;

        public BuildingListVM(IService<Building> buildingService, IService<Room> roomService)
        {
            _buildingService = buildingService;
            _roomService = roomService;
        }

        public async Task LoadBuildings()
        {
            Buildings = (await _buildingService.GetAllAsync()).ToList();
            Buildings.ForEach(b => b.Rooms.ForEach(r => r.Building = b));
        }

        public async Task DeleteBuilding(Building building)
        {
            await _buildingService.DeleteAsync(building.Id);
            Buildings.Remove(building);
        }

        public async Task DeleteRoom(Room room)
        {
            await _roomService.DeleteAsync(room.Id);
            room.Building.Rooms.Remove(room);
        }
    }
}
