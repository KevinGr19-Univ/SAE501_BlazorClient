using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.ViewModels
{
    public class BuildingListVM
    {
        public IList<Building> Buildings { get; set; } = null;

        public async Task LoadBuildings()
        {
            await Task.Delay(500);

            IList<Building> buildings = [
                new()
                {
                    Name = "IUT Annecy",
                    Rooms = [
                        new()
                        {
                            Name = "D351",
                            Objects = [
                                new Door(),
                                new Door(),
                                new Window(),
                                new Window(),
                                new Window(),
                                new Heater(),

                                new Lamp(),
                                new Plug(),
                                new Plug(),

                                new Sensor(),
                                new Sensor(),
                            ]
                        },
                        new() { Name = "D360" },
                    ]
                }
            ];

            Buildings = buildings;
        }
    }
}
