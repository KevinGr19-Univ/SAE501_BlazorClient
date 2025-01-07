using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Services
{
    public interface IAPIService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Building?> GetBuildingAsync(string idBuilding);
        Task<Room?> GetRoomAsync(Guid guid);

        Task<Room> PostRoomAsync(Room room);
        Task<Room> PutRoomAsync(Guid idRoom, Room room);

        Task<Building> PostBuildingAsync(Building building);
        Task<Building> PutBuildingAsync(string idBuilding, Building building);
    }
}
