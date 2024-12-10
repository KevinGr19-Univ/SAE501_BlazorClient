using ClientBlazor_v1.Models.RoomObjects;

namespace ClientBlazor_v1.Utils
{
    public class RoomObjectSorter<T>
    {
        public ICollection<T> Sensors { get; init; }
        public ICollection<T> Actionnables { get; init; }
        public ICollection<T> Equipments { get; init; }

        public static RoomObjectSorter<T> SortObjects(IEnumerable<T> elements, Func<T, RoomObject> objectGetter)
        {
            var sensors = new List<T>();
            var actionnables = new List<T>();
            var equipments = new List<T>();

            foreach (var element in elements)
            {
                var roomObject = objectGetter(element);
                if (roomObject is Sensor sensor) sensors.Add(element);
                else if (roomObject is ActionnableObject actionnable) actionnables.Add(element);
                else equipments.Add(element);
            }

            return new()
            {
                Sensors = sensors,
                Actionnables = actionnables,
                Equipments = equipments
            };
        }
    }
}
