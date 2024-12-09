namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Lamp : Actionnable
    {
        public string Type { get; set; }

        public override string GetName()
        {
            return string.Format("Lampe {0}", Type);
        }
    }
}
