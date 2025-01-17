using ClientBlazor_v1.ViewModels.JS.RoomObjects;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClientBlazor_v1.Models.RoomObjects
{
    public abstract class RoomObject : ICloneable
    {
        public static readonly IList<Type> SUBTYPES = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsAssignableTo(typeof(RoomObject)) && !t.IsAbstract).ToArray().AsReadOnly();

        public int Id { get; set; }

        public string? CustomName { get; set; }

        [DeniedValues(0, ErrorMessage = "La salle de l'objet est requise")]
        public int IdRoom { get; set; }

        public Room Room { get; set; }

        abstract public string GetRootName();
        public string GetFullName() => GetRootName() + (string.IsNullOrEmpty(CustomName) ? "" : $" \"{CustomName}\"");

        public object Clone() => MemberwiseClone();

        abstract public RoomObjectVM ToVM();
        abstract public string GetJSBuilderName();
    }
}
