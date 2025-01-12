using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingCreatorVM
    {
        private readonly IService<Building> _buildingService;

        public bool IsLoaded { get; private set; } = false;

        private int? _idBuilding;
        public Building Building { get; set; }

        public BuildingCreatorVM(IService<Building> api)
        {
            _buildingService = api;
        }

        public async Task Load(int? idBuilding)
        {
            IsLoaded = false;

            _idBuilding = idBuilding;
            Building = _idBuilding is null ? new Building() : await _buildingService.GetByIdAsync((int)_idBuilding);

            IsLoaded = true;
        }

        public async Task SaveBuilding()
        {
            if(_idBuilding is null) await _buildingService.PostAsync(Building);
            else await _buildingService.PutAsync((int)_idBuilding, Building);
        }
    }
}
