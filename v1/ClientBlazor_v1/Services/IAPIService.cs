using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Services
{
    public interface IAPIService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Building?> GetBuildingAsync(int idBuilding);
        Task<Building> PostBuildingAsync(Building building);
        Task PutBuildingAsync(int idBuilding, Building building);

        Task<Room?> GetRoomAsync(int idRoom);
        Task<Room> PostRoomAsync(Room room);
        Task PutRoomAsync(int idRoom, Room room);

        Task<IEnumerable<RoomType>> GetRoomTypesAsync();
        Task<RoomType?> GetRoomTypeAsync(int idRoomType);
        Task<RoomType> PostRoomTypeAsync(RoomType roomType);
        Task PutRoomTypeAsync(int idRoomType, RoomType roomType);
        Task DeleteRoomTypeAsync(int idRoomType);
    }
}
