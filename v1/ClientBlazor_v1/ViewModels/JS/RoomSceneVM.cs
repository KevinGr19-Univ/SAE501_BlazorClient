using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.ViewModels.JS.RoomObjectVM;
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

        public readonly IList<IJSObjectVM> ObjectVMs;

        public RoomSceneVM(IJSInProcessObjectReference sceneObj)
        {
            JSObj = sceneObj;
            ObjectVMs = new List<IJSObjectVM>();
        }

        public DoorVM AddDoor(Door door)
        {
            var doorVM = new DoorVM();
            doorVM.Door = door;
            doorVM.JSObj = JSObj.Invoke<IJSInProcessObjectReference>("addDoor");
            ObjectVMs.Add(doorVM);

            return doorVM;
        }

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
            }
        }

        [JSInvokable]
        public async Task ElementSelected(DotNetObjectReference<object> dotnetRef)
        {
            Console.WriteLine("ElementSelected");
        }
    }
}
