using ClientBlazor_v1.Models;
using ClientBlazor_v1.Services;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingCreatorVM
    {
        private readonly IAPIService _api;

        public bool IsLoaded { get; private set; } = false;

        private string? _idBuilding;
        public Building Building { get; set; }

        public BuildingCreatorVM(IAPIService api)
        {
            _api = api;
        }

        public async Task Load(string? idBuilding)
        {
            IsLoaded = false;

            _idBuilding = idBuilding;
            Building = _idBuilding is null ? new Building() : await _api.GetBuildingAsync(_idBuilding);

            IsLoaded = true;
        }

        public async Task SaveBuilding()
        {
            if(_idBuilding is null) await _api.PostBuildingAsync(Building);
            else await _api.PutBuildingAsync(_idBuilding, Building);
        }
    }
}
