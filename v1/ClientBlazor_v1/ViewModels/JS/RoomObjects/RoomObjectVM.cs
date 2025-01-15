using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Models.Transform;
using ClientBlazor_v1.Utils;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace ClientBlazor_v1.ViewModels.JS.RoomObjects
{
    public abstract class RoomObjectVM : JSObjectVM
    {
        private RoomObject _object;
        public RoomObject Object
        {
            get => _object;
            set
            {
                _object = value;
                RequireUIUpdate();
            }
        }

        public event EventHandler OnSelect;
        public event EventHandler OnClose;
        public event EventHandler OnMarkedForDeletionChanged;

        public void Select()
        {
            JSObj.InvokeVoid("sceneSelect");
            OnSelect?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            JSObj.InvokeVoid("sceneUnselect");
            OnClose?.Invoke(this, EventArgs.Empty);
        }

        public bool IsNew => Object.Id == 0;
        public bool MarkedForDeletion { get; private set; }

        public void SetMarkedForDeletion(bool delete)
        {
            MarkedForDeletion = delete;
            OnMarkedForDeletionChanged?.Invoke(this, EventArgs.Empty);
            JSObj.InvokeVoid("setMarkedForDeletion", delete);
        }

        public Func<RoomObject, Task> DuplicateHandler { get; set; }
        public async Task Duplicate()
        {
            var duplicate = (RoomObject)Object.Clone();
            var original = Object;

            _object = duplicate;
            ApplyVMTOObject();
            _object = original;

            await DuplicateHandler?.Invoke(duplicate);
        }

        public bool TryValidateModel()
        {
            var context = new ValidationContext(Object);
            return Validator.TryValidateObject(Object, context, null);
        }

        public virtual void ApplyObjectToVM(bool ignoreNew)
        {
            if(!IsNew || ignoreNew) TransformCopySourceToTarget(Object, this);
        }

        public virtual void ApplyVMTOObject()
        {
            TransformCopySourceToTarget(this, Object);
        }

        private void TransformCopySourceToTarget(object source, object target)
        {
            if (target is IPosition targetPos) targetPos.CopyPosFrom((IPosition)source);
            if (target is IRotation targetRot) targetRot.CopyRotFrom((IRotation)source);
            if (target is IOrientation targetOrt) targetOrt.Orientation = ((IOrientation)source).Orientation;
            if (target is ISize targetSize) targetSize.CopySizeFrom((ISize)source);
        }

        #region Common utils
        public double GetPosX() => JSGet<double>("position.x");
        public double GetPosY() => JSGet<double>("position.y");
        public double GetPosZ() => JSGet<double>("position.z");

        public double GetRotX() => JSGet<double>("rotation.x") * MathUtils.RAD2DEG;
        public double GetRotY() => JSGet<double>("rotation.y") * MathUtils.RAD2DEG;
        public double GetRotZ() => JSGet<double>("rotation.z") * MathUtils.RAD2DEG;

        public double GetSizeX() => JSGet<double>("size.x");
        public double GetSizeY() => JSGet<double>("size.y");
        public double GetSizeZ() => JSGet<double>("size.z");

        public void SetPosX(double value) => JSSet("position.x", value);
        public void SetPosY(double value) => JSSet("position.y", value);
        public void SetPosZ(double value) => JSSet("position.z", value);

        public void SetRotX(double value) => JSSet("rotation.x", value * MathUtils.DEG2RAD);
        public void SetRotY(double value) => JSSet("rotation.y", value * MathUtils.DEG2RAD);
        public void SetRotZ(double value) => JSSet("rotation.z", value * MathUtils.DEG2RAD);

        public void SetSizeX(double value) => JSSet("size.x", value);
        public void SetSizeY(double value) => JSSet("size.y", value);
        public void SetSizeZ(double value) => JSSet("size.z", value);
        #endregion
    }

    public abstract class RoomObjectVM<TRoomObject> : RoomObjectVM where TRoomObject : RoomObject
    {
        public new TRoomObject Object
        {
            get => (TRoomObject)base.Object;
            set => base.Object = value;
        }
    }
}
