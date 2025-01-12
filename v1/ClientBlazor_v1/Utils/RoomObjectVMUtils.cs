using ClientBlazor_v1.ViewModels.JS.RoomObject;

namespace ClientBlazor_v1.Utils
{
    public static class RoomObjectVMUtils
    {
        public static double GetPosX(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.x");
        public static double GetPosY(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.y");
        public static double GetPosZ(RoomObjectVM jsObj) => jsObj.JSGet<double>("position.z");

        public static double GetRotX(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.x") * MathUtils.RAD2DEG;
        public static double GetRotY(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.y") * MathUtils.RAD2DEG;
        public static double GetRotZ(RoomObjectVM jsObj) => jsObj.JSGet<double>("rotation.z") * MathUtils.RAD2DEG;

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
            jsObj.JSSet("rotation.x", value * MathUtils.DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotY(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.y", value * MathUtils.DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotZ(RoomObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.z", value * MathUtils.DEG2RAD);
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
