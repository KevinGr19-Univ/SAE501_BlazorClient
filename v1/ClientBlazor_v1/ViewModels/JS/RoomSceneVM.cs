using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.RoomObjects.ConnectedObjects;
using ClientBlazor_v1.Services;
using ClientBlazor_v1.ViewModels.JS.RoomObjects;
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

            CreateInputSelectRoomObjectTypes();
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
        private class RoomObjectVMBuilder
        {
            public string JSBuilderName { get; init; }
            public Func<RoomObject, RoomObjectVM> VMBuilder { get; init; }
        }

        private readonly Dictionary<Type, RoomObjectVMBuilder> _roomObjectVMBuilders = new()
        {
            { typeof(Door), new() {
                JSBuilderName = "addDoor",
                VMBuilder = (obj) => new DoorVM() { Object = (Door)obj } 
            }},
            { typeof(Table), new() {
                JSBuilderName = "addTable",
                VMBuilder = (obj) => new TableVM() { Object = (Table)obj } 
            }},
            { typeof(Heater), new() {
                JSBuilderName = "addHeater",
                VMBuilder = (obj) => new HeaterVM() { Object = (Heater)obj } 
            }},
            { typeof(Window), new() {
                JSBuilderName = "addWindow",
                VMBuilder = (obj) => new WindowVM() { Object = (Window)obj } 
            }},
            { typeof(Sensor6in1), new() {
                JSBuilderName = "addSensor6in1",
                VMBuilder = (obj) => new Sensor6in1VM() { Object = (Sensor6in1)obj } 
            }},
            { typeof(Sensor9in1), new() {
                JSBuilderName = "addSensor9in1",
                VMBuilder = (obj) => new Sensor9in1VM() { Object = (Sensor9in1)obj } 
            }},
            { typeof(SensorCO2), new() {
                JSBuilderName = "addSensorCO2",
                VMBuilder = (obj) => new SensorCO2VM() { Object = (SensorCO2)obj } 
            }},
            { typeof(Lamp), new() {
                JSBuilderName = "addLamp",
                VMBuilder = (obj) => new LampVM() { Object = (Lamp)obj } 
            }},
            { typeof(Plug), new() {
                JSBuilderName = "addPlug",
                VMBuilder = (obj) => new PlugVM() { Object = (Plug)obj } 
            }},
            { typeof(Siren), new() {
                JSBuilderName = "addSiren",
                VMBuilder = (obj) => new SirenVM() { Object = (Siren)obj }
            }},
        };

        public IList<RoomObject> InputSelect_RoomObjectTypes { get; private set; }
        public int RoomObjectType_SelectedIndex { get; set; } = -1;
        public RoomObject? RoomObjectType_Selected => RoomObjectType_SelectedIndex < 0 ? null : InputSelect_RoomObjectTypes[RoomObjectType_SelectedIndex];

        private void CreateInputSelectRoomObjectTypes()
        {
            InputSelect_RoomObjectTypes = _roomObjectVMBuilders.Keys
                .Select(t => (RoomObject)t.GetConstructor([])!.Invoke(null))
                .ToList().AsReadOnly();
        }

        public void AddNewOfSelectedRoomObjectType()
        {
            var roomObj = RoomObjectType_Selected;
            if (roomObj is null) return;

            RoomObjectType_SelectedIndex = -1;
            CreateInputSelectRoomObjectTypes();
            AddRoomObjectVM(roomObj);
        }

        private void AddRoomObjectVM(RoomObject roomObj)
        {
            var vmBuilder = _roomObjectVMBuilders[roomObj.GetType()];
            var vm = vmBuilder.VMBuilder(roomObj);
            vm.JSObj = JSObj.Invoke<IJSInProcessObjectReference>(vmBuilder.JSBuilderName);

            vm.OnSelect += OnVMSelect;
            vm.OnClose += OnVMClose;
            ObjectVMs.Add(vm);
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
            foreach (var roomObj in Room.ObjectsOfRoom) AddRoomObjectVM(roomObj);
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
