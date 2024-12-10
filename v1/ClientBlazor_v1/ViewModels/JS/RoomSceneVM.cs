using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.ViewModels.JS.RoomObject;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels.JS
{
    public class RoomSceneVM : JSObjectVM
    {
        private Room _room;
        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                UpdateRoomMesh();
                UpdateRoomObjects();
                RequireUIUpdate();
            }
        }

        public readonly ICollection<RoomObjectVM> ObjectVMs = new HashSet<RoomObjectVM>();
        public readonly ICollection<RoomObjectVM> VisibleObjectVMs = new HashSet<RoomObjectVM>();

        public RoomSceneVM(IJSInProcessObjectReference sceneObj)
        {
            JSObj = sceneObj;
        }

        public DoorVM AddDoor(Door door) => AddRoomObject<DoorVM>(() => new() { Door = door }, "addDoor");
        public SensorVM AddSensor(Sensor sensor) => AddRoomObject<SensorVM>(() => new() { Sensor = sensor }, "addSensor");

        private T AddRoomObject<T>(Func<T> vmBuilder, string jsBuilderName) where T : RoomObjectVM
        {
            T vm = vmBuilder();
            vm.JSObj = JSObj.Invoke<IJSInProcessObjectReference>(jsBuilderName);

            vm.OnSelect += OnVMSelect;
            vm.OnClose += OnVMClose;
            ObjectVMs.Add(vm);
            return vm;
        }

        private void AddObjectVMToVisible(RoomObjectVM objectVM)
        {
            if (!VisibleObjectVMs.Contains(objectVM))
            {
                VisibleObjectVMs.Add(objectVM);
                RequireUIUpdate();
            }
        }

        #region Updates
        public void UpdateRoomMesh()
        {
            JSObj.InvokeVoid("updateRoomMesh", Room.Height);
        }

        public void UpdateRoomObjects()
        {
            ObjectVMs.Clear();
            foreach (var roomObj in Room.Objects)
            {
                if (roomObj is Door door) AddDoor(door);
                else if (roomObj is Sensor sensor) AddSensor(sensor);
            }
        }
        #endregion

        #region Events
        private void OnVMSelect(object? obj, EventArgs e)
        {
            if(obj is RoomObjectVM objectVM)
            {
                AddObjectVMToVisible(objectVM);
            }
        }

        private void OnVMClose(object? obj, EventArgs e)
        {
            if (obj is RoomObjectVM objectVM)
            {
                VisibleObjectVMs.Remove(objectVM);
                RequireUIUpdate();
            }
        }

        [JSInvokable]
        public async Task ElementSelected(DotNetObjectReference<object> dotnetRef)
        {
            RoomObjectVM objectVM = (RoomObjectVM)dotnetRef.Value;
            AddObjectVMToVisible(objectVM);
        }
        #endregion
    }
}
