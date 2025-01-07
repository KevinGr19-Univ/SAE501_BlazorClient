using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Services
{
    public interface IAPIService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Building?> GetBuildingAsync(int idBuilding);
        Task<Room?> GetRoomAsync(int idRoom);

        Task<Room> PostRoomAsync(Room room);
        Task<Room> PutRoomAsync(int idRoom, Room room);

        Task<Building> PostBuildingAsync(Building building);
        Task<Building> PutBuildingAsync(int idBuilding, Building building);
    }
}
