namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Siren : Actionnable
    {
        public string Type { get; set; }

        public override string GetName()
        {
            return string.Format("Sirène", Type);
        }
    }
}
