using ClientBlazor_v1.Models;

namespace ClientBlazor_v1.ViewModels
{
    public class RoomVM
    {
        public Room Room { get; set; }

        public async Task<bool> DeleteAsync()
        {
            Console.WriteLine($"Deleting Room {Room.Name}");
            return true;
        }
    }
}
