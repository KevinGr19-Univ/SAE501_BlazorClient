﻿using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
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

        private int _idRoom;
        public async Task LoadRoom(int idRoom)
        {
            Room? room = await _roomService.GetByIdAsync(idRoom);
            _idRoom = idRoom;
            _room = room;

            if (Room is not null)
            {
                _room.ObjectsOfRoom.Sort(new Comparison<RoomObject>((obj1, obj2) => Comparer<int>.Default.Compare(obj1.Id, obj2.Id)));

                UpdateRoomMesh();
                await UpdateRoomObjects();
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

        #region Add/Delete RoomObject
        public IList<RoomObject> InputSelect_RoomObjectTypes { get; private set; }
        public int RoomObjectType_SelectedIndex { get; set; } = -1;
        public RoomObject? RoomObjectType_Selected => RoomObjectType_SelectedIndex < 0 ? null : InputSelect_RoomObjectTypes[RoomObjectType_SelectedIndex];

        private void CreateInputSelectRoomObjectTypes()
        {
            InputSelect_RoomObjectTypes = RoomObject.SUBTYPES
                .Select(t => (RoomObject)t.GetConstructor([])!.Invoke(null))
                .ToList().AsReadOnly();
        }

        public async Task AddNewOfSelectedRoomObjectType()
        {
            var roomObj = RoomObjectType_Selected;
            if (roomObj is null) return;

            RoomObjectType_SelectedIndex = -1;
            CreateInputSelectRoomObjectTypes();

            var vm = await AddRoomObjectVM(roomObj);
            vm.Select();
        }

        private async Task<RoomObjectVM> AddRoomObjectVM(RoomObject roomObj, bool duplicated = false)
        {
            var vm = roomObj.ToVM();
            vm.JSObj = await JSObj.InvokeAsync<IJSInProcessObjectReference>(roomObj.GetJSBuilderName());
            vm.ApplyObjectToVM(duplicated);

            vm.OnSelect += OnVMSelect;
            vm.OnClose += OnVMClose;
            vm.OnMarkedForDeletionChanged += OnVMMarkedForDeletionChanged;

            vm.DuplicateHandler = DuplicateRoomObject;

            ObjectVMs.Add(vm);
            return vm;
        }

        private async Task DuplicateRoomObject(RoomObject newRoomObject)
        {
            newRoomObject.Id = 0;

            var newName = newRoomObject.CustomName;
            if(string.IsNullOrEmpty(newName)) newName = "Copie";
            List<string> parts = newName.Split('.').ToList();

            HashSet<string?> reservedNames = ObjectVMs
                .Select(vm => vm.Object.CustomName)
                .ToHashSet();

            if (parts.Count == 1)
            {
                newRoomObject.CustomName = $"{newName}.1";
                parts.Add("1");
            }

            while(reservedNames.Contains(newRoomObject.CustomName))
            {
                if (int.TryParse(parts[^1], out int number))
                {
                    string newPart = $"{number + 1}";
                    newRoomObject.CustomName = $"{string.Join(".", parts[..^1])}.{newPart}";
                    parts.RemoveAt(parts.Count-1);
                    parts.Add(newPart);
                }

                else
                {
                    newRoomObject.CustomName = $"{newRoomObject.CustomName}.1";
                    parts.Add("1");
                }
            }

            var vm = await AddRoomObjectVM(newRoomObject, true);
            vm.Select();
        }

        private void AddObjectVMToVisible(RoomObjectVM objectVM)
        {
            if (!VisibleObjectVMs.Contains(objectVM))
            {
                VisibleObjectVMs.Clear(); // Kept visibleVMs as a collection for future changes
                VisibleObjectVMs.Add(objectVM);
                RequireUIUpdate();
            }
        }

        private void DeleteRoomObjectVM(RoomObjectVM vm)
        {
            ObjectVMs.Remove(vm);
            VisibleObjectVMs.Remove(vm);
            vm.OnSelect -= OnVMSelect;
            vm.OnClose -= OnVMClose;
            vm.OnMarkedForDeletionChanged -= OnVMMarkedForDeletionChanged;

            vm.JSObj.InvokeVoid("deleteSelf");
            vm.Dispose();
        }
        #endregion

        #region Save
        public async Task SaveChanges()
        {
            // TODO: Object validation
            List<RoomObjectVM> objectVMs = ObjectVMs.Where(vm => !vm.MarkedForDeletion).ToList();
            objectVMs.ForEach(vm => vm.ApplyVMTOObject());

            var newRoom = (Room)Room.Clone();
            newRoom.ObjectsOfRoom = objectVMs.Select(vm => vm.Object).ToList();

            await _roomService.PutAsync(_idRoom, newRoom);
            await LoadRoom(_idRoom);
        }
        #endregion

        #region Updates
        public void UpdateRoomMesh()
        {
            object points = Room.Base.Select(v => new {x = v.X, y = v.Y}).ToArray();
            JSObj.InvokeVoid("updateRoomMesh", points, Room.Height);
        }

        public async Task UpdateRoomObjects()
        {
            JSObj.InvokeVoid("clearRoomObjects");
            foreach (var vm in ObjectVMs) DeleteRoomObjectVM(vm);

            if(Room is not null) 
                await Task.WhenAll(Room.ObjectsOfRoom.Select(roomObj => AddRoomObjectVM(roomObj)));
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

        private void OnVMMarkedForDeletionChanged(object? obj, EventArgs e)
        {
            if(obj is RoomObjectVM objectVM)
            {
                if (objectVM.IsNew && objectVM.MarkedForDeletion) DeleteRoomObjectVM(objectVM);
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
