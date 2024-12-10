namespace ClientBlazor_v1.ViewModels.JS.RoomObject
{
    public static class RoomObjectVMUtils
    {
        private const double RAD2DEG = 180 / Math.PI;
        private const double DEG2RAD = Math.PI / 180;

        public static double GetPosX(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.x");
        public static double GetPosY(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.y");
        public static double GetPosZ(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.z");

        public static double GetRotX(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.x") * RAD2DEG;
        public static double GetRotY(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.y") * RAD2DEG;
        public static double GetRotZ(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.z") * RAD2DEG;

        public static double GetSizeX(RoomObjectVM jsObj) => jsObj.JSGet<double>("size.x");
        public static double GetSizeY(RoomObjectVM jsObj) => jsObj.JSGet<double>("size.y");
        public static double GetSizeZ(RoomObjectVM jsObj) => jsObj.JSGet<double>("size.z");

        public static void SetPosX(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.x", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetPosY(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.y", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetPosZ(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.z", value);
            jsObj.RequireUIUpdate();
        }

        public static void SetRotX(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.x", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotY(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.y", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotZ(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.z", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }

        public static void SetSizeX(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.x", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetSizeY(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.y", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetSizeZ(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.z", value);
            jsObj.RequireUIUpdate();
        }
    }
}
