namespace ClientBlazor_v1.ViewModels.JS.RoomObjectVM
{
    public static class RoomObjectVMUtils
    {
        private const double RAD2DEG = 180 / Math.PI;
        private const double DEG2RAD = Math.PI / 180;

        public static double GetPosX(IJSObjectVM jsObj) => jsObj.JSGet<double>("position.x");
        public static double GetPosY(IJSObjectVM jsObj) => jsObj.JSGet<double>("position.y");
        public static double GetPosZ(IJSObjectVM jsObj) => jsObj.JSGet<double>("position.z");

        public static double GetRotX(IJSObjectVM jsObj) => jsObj.JSGet<double>("rotation.x") * RAD2DEG;
        public static double GetRotY(IJSObjectVM jsObj) => jsObj.JSGet<double>("rotation.y") * RAD2DEG;
        public static double GetRotZ(IJSObjectVM jsObj) => jsObj.JSGet<double>("rotation.z") * RAD2DEG;

        public static double GetSizeX(IJSObjectVM jsObj) => jsObj.JSGet<double>("size.x");
        public static double GetSizeY(IJSObjectVM jsObj) => jsObj.JSGet<double>("size.y");
        public static double GetSizeZ(IJSObjectVM jsObj) => jsObj.JSGet<double>("size.z");

        public static void SetPosX(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.x", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetPosY(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.y", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetPosZ(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("position.z", value);
            jsObj.RequireUIUpdate();
        }

        public static void SetRotX(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.x", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotY(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.y", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }
        public static void SetRotZ(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("rotation.z", value * DEG2RAD);
            jsObj.RequireUIUpdate();
        }

        public static void SetSizeX(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.x", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetSizeY(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.y", value);
            jsObj.RequireUIUpdate();
        }
        public static void SetSizeZ(IJSObjectVM jsObj, double value)
        {
            jsObj.JSSet("size.z", value);
            jsObj.RequireUIUpdate();
        }
    }
}
