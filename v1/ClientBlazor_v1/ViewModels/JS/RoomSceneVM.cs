using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.RoomObjects.ConnectedObjects;
using ClientBlazor_v1.Services;
using ClientBlazor_v1.ViewModels.JS.RoomObject;
using Microsoft.JSInterop;

namespace ClientBlazor_v1.ViewModels.JS
{
    public class RoomSceneVM : JSObjectVM
    {
        private readonly IService<Room> _roomService;

        private Room? _room;
        public Room? Room => _room;

        public readonly ICollection<RoomObjectVM> ObjectVMs = new HashSet<RoomObjectVM>();
        public readonly ICollection<RoomObjectVM> VisibleObjectVMs = new HashSet<RoomObjectVM>();
        public readonly CompassVM CompassVM;

        public RoomSceneVM(IService<Room> roomService, IJSInProcessObjectReference sceneObj)
        {
            _roomService = roomService;
            JSObj = sceneObj;

            CompassVM = new();
            CompassVM.JSObj = sceneObj.Invoke<IJSInProcessObjectReference>("getCamera");
        }

        public async Task LoadRoom(int idRoom)
        {
            Room? room = await _roomService.GetByIdAsync(idRoom);
            _room = room;

            if (Room is not null)
            {
                UpdateRoomMesh();
                UpdateRoomObjects();
                RequireUIUpdate();
            }
        }

        #region Buttons
        public void SetGizmoPos() => JSObj.InvokeVoid("setGizmoPos");
        public void SetGizmoRot() => JSObj.InvokeVoid("setGizmoRot");
        public void SetGizmoScale() => JSObj.InvokeVoid("setGizmoScale");
        public void SetFocusToCenter() => JSObj.InvokeVoid("setFocusToCenter");
        public void SetFocusToSelected() => JSObj.InvokeVoid("setFocusToSelected");
        #endregion

        #region Add RoomObject
        private readonly Dictionary<string, Type> _roomObjectTypes = new();
        public IEnumerable<string> RoomObjectTypes => _roomObjectTypes.Keys;
        public string RoomObjectTypeToAdd { get; set; }

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
        #endregion

        #region Updates
        public void UpdateRoomMesh()
        {
            object points = Room.Base.Select(v => new {x = v.X, y = v.Y}).ToArray();
            JSObj.InvokeVoid("updateRoomMesh", points, Room.Height);
        }

        public void UpdateRoomObjects()
        {
            ObjectVMs.Clear();
            foreach (var roomObj in Room.ObjectsOfRoom)
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
