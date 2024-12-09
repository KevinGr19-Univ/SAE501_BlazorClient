namespace ClientBlazor_v1.Models.RoomObjects
{
    public class Sensor : RoomObject
    {
        public string Type { get; set; }

        public override string GetName()
        {
            return string.Format("Capteur {0}", Type);
        }
    }
}
