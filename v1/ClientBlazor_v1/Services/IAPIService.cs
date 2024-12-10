using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.Services
{
    public interface IAPIService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Room?> GetRoomAsync(Guid guid);
    }
}
