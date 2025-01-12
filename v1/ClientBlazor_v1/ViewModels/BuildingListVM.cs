using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingListVM
    {
        private readonly IService<Building> _buildingService;
        public List<Building> Buildings { get; set; } = null;

        public BuildingListVM(IService<Building> buildingService)
        {
            _buildingService = buildingService;
        }

        public async Task LoadBuildings()
        {
            Buildings = (await _buildingService.GetAllAsync()).ToList();
        }
    }
}
