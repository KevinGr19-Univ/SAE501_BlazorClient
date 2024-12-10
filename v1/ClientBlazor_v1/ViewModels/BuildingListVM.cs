using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingListVM
    {
        private readonly IAPIService _api;
        public IEnumerable<Building> Buildings { get; set; } = null;

        public BuildingListVM(IAPIService api)
        {
            _api = api;
        }

        public async Task LoadBuildings()
        {
            Buildings = await _api.GetBuildingsAsync();
        }
    }
}
