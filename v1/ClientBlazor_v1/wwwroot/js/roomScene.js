import { clamp, addSizeProps, isClockwiseXZ } from './utils.js';
import { dotnetProxify, addDotnetMutators } from './interop.js';
import { objectInfos, startLoadingModels } from './objectInfos.js';
import earcut from 'https://cdn.jsdelivr.net/npm/earcut@3.0.1/+esm';

class RoomScene {

    constructor(canvas) {
        this.canvas = canvas;
        this.engine = new BABYLON.Engine(this.canvas, true, { stencil: true });
        this.engine.adaptToDeviceRatio = true;

        this.scene = new BABYLON.Scene(this.engine);
        this.scene.useRightHandedSystem = true;
        this.scene.clearColor = new BABYLON.Color3(0.3, 0.35, 0.4);

        this.camera = new BABYLON.ArcRotateCamera("camera1", 90, 45, 10, BABYLON.Vector3.Zero());
        this.camera.allowUpsideDown = false;
        this.camera.minZ = 0.01;
        this.camera.setPosition(BABYLON.Vector3.One().scale(10));
        this.camera.attachControl(this.canvas, true);
        this.camera.wheelPrecision = 30;

        addDotnetMutators(this.camera);
        this.scene.addCamera(this.camera);

        this.ambientLight = new BABYLON.HemisphericLight("ambientLight", new BABYLON.Vector3(0, 0, 0), this.scene);
        this.pointLight = new BABYLON.PointLight("pointLight", new BABYLON.Vector3(0, 3, 3), this.scene);
        this.ambientLight.intensity = 0.9;
        this.pointLight.intensity = 0.3;

        BABYLON.SceneLoader.ShowLoadingScreen = false;
        startLoadingModels(this.scene);
        this.initGizmo();

        this.engine.runRenderLoop(() => this.scene.render());
        window.addEventListener('resize', () => this.engine.resize());

        this.scene.onAfterRenderCameraObservable.add(() => {
            let direction = this.camera.getDirection(new BABYLON.Vector3(0, 0, 1));
            let angle = Math.atan2(direction.z, direction.x);
            if (angle < 0) angle = Math.PI*2 + angle;

            this.camera.orientation = angle;
            if (this.camera.dotnetRef) this.camera.dotnetRef.invokeMethod("RequireUIUpdate");
        });

        this.roomObjects = new Set();
    }

    updateRoomMesh(points, height) {
        if (this.room) {
            this.room.dispose();
        }

        // Set points in the right order (for normals)
        let shape = points.map(p => new BABYLON.Vector3(-p.y, 0, -p.x));
        if (isClockwiseXZ(shape)) shape.reverse();

        this.room = BABYLON.MeshBuilder.ExtrudePolygon("room", {
            shape: shape,
            depth: height,
        }, this.scene, earcut);

        let center = this.room.getBoundingInfo().boundingBox.center;
        this.room.setPivotMatrix(BABYLON.Matrix.Translation(-center.x, height, -center.z), false);

        this.room.material = new BABYLON.StandardMaterial("roomMat");
        this.room.flipFaces(true);
        this.room.isPickable = false;

        setTimeout(() => this.setFocusToCenter(), 1);
    }

    clearRoomObjects() {
        [...this.roomObjects].forEach(obj => obj.deleteSelf());
    }

    initGizmo() {
        this.gizmoManager = new BABYLON.GizmoManager(this.scene);
        this.scene.layers.push(this.gizmoManager.utilityLayer);
        console.log(this.scene.layers);
        console.log("----------------");

        this.gizmoManager.positionGizmoEnabled = true;
        this.gizmoManager.rotationGizmoEnabled = true;
        this.gizmoManager.scaleGizmoEnabled = true;
        this.resetGizmo();

        this.gizmoManager.clearGizmoOnEmptyPointerEvent = false;
        this.gizmoManager.updateGizmoRotationToMatchAttachedMesh = false;

        this.gizmoManager.onAttachedToMeshObservable.add(mesh => {
            if (mesh && !mesh.__isProxy && mesh.proxy) {
                mesh = mesh.proxy;
                this.setSelected(mesh);

                this.gizmoManager.attachToMesh(mesh);
                if (mesh.dotnetRef) this.dotnetRef?.invokeMethodAsync("ElementSelected", mesh.dotnetRef);
            }
            else if (!mesh) {
                this.setSelected(null);
            }
        });

        this.canvas.addEventListener("keydown", (e) => {
            if (e.key == 'g') this.setGizmoPos();
            else if (e.key == 'r') this.setGizmoRot();
            else if (e.key == 's') this.setGizmoScale();
        });

        this.gizmoManager.attachableMeshes = [];

        this.gizmoManager.gizmos.positionGizmo.onDragObservable.add(() => {
            var bb_room = this.room.getBoundingInfo().boundingBox;

            var mesh = this.gizmoManager.attachedMesh;
            var { min, max } = mesh.getHierarchyBoundingVectors();
            var size = max.subtract(min).scale(0.5);

            for (let dim of ["x", "y", "z"]) {
                mesh.position[dim] = clamp(
                    mesh.position[dim],
                    bb_room.minimumWorld[dim] + size[dim],
                    bb_room.maximumWorld[dim] - size[dim],
                );
            }
        });
    }

    setSelected(mesh) {
        if (this.selected === mesh) return;

        if (this.selected) {
            this.selected.material.emissiveColor = BABYLON.Color3.Black();
        }

        this.selected = mesh;
        this.gizmoManager.attachToMesh(this.selected);

        if (this.selected) {
            this.selected.material.emissiveColor = new BABYLON.Color3(0.3, 0.3, 0.3);

            let bindedProps = objectInfos[this.selected.roomObjectKey].bindedProps;
            this.gizmoManager.gizmos.positionGizmo.xGizmo.isEnabled = bindedProps.position?.x;
            this.gizmoManager.gizmos.positionGizmo.yGizmo.isEnabled = bindedProps.position?.y;
            this.gizmoManager.gizmos.positionGizmo.zGizmo.isEnabled = bindedProps.position?.z;

            this.gizmoManager.gizmos.rotationGizmo.xGizmo.isEnabled = bindedProps.rotation?.x;
            this.gizmoManager.gizmos.rotationGizmo.yGizmo.isEnabled = bindedProps.rotation?.y;
            this.gizmoManager.gizmos.rotationGizmo.zGizmo.isEnabled = bindedProps.rotation?.z;

            this.gizmoManager.gizmos.scaleGizmo.xGizmo.isEnabled = bindedProps.size?.x;
            this.gizmoManager.gizmos.scaleGizmo.yGizmo.isEnabled = bindedProps.size?.y;
            this.gizmoManager.gizmos.scaleGizmo.zGizmo.isEnabled = bindedProps.size?.z;
            this.gizmoManager.gizmos.scaleGizmo.uniformScaleGizmo.isEnabled =
                (bindedProps.size?.x) && (bindedProps.size?.y) && (bindedProps.size?.z);
        }

        this.gizmoManager.attachToMesh(this.selected);
    }

    addDoor() { return this._addRoomObject("door"); }
    addTable(){ return this._addRoomObject("table"); }
    addWindow(){ return this._addRoomObject("window"); }
    addHeater(){ return this._addRoomObject("heater"); }
    addSensor6in1(){ return this._addRoomObject("sensor6in1"); }
    addSensor9in1(){ return this._addRoomObject("sensor9in1"); }
    addSensorCO2(){ return this._addRoomObject("sensorCO2"); }
    addLamp(){ return this._addRoomObject("lamp"); }
    addPlug(){ return this._addRoomObject("plug"); }
    addSiren() { return this._addRoomObject("siren"); }

    async _addRoomObject(key) {
        let mesh = await objectInfos[key].meshBuilder(this);
        this.scene.addMesh(mesh);
        mesh.position = this.getSceneCenter();

        this.gizmoManager.attachableMeshes.push(mesh);
        mesh.roomObjectKey = key;
        addSizeProps(mesh);

        let obj = dotnetProxify(mesh, objectInfos[key].bindedProps);
        this.roomObjects.add(obj);

        obj.sceneSelect = () => this.setSelected(obj);

        obj.sceneUnselect = () => {
            if (this.selected === obj) this.setSelected(null);
        }

        obj.deleteSelf = () => {
            obj.sceneUnselect();
            obj.dispose();

            this.roomObjects.delete(obj);
            let i = this.gizmoManager.attachableMeshes.indexOf(mesh);
            if (i >= 0) this.gizmoManager.attachableMeshes.splice(i, 1);
        }

        obj.setMarkedForDeletion = (deletion) => {
            obj.material.alpha = deletion ? 0.3 : 1;
        }

        return obj;
    }

    getCamera() {
        return this.camera;
    }

    resetGizmo() {
        this.gizmoManager.positionGizmoEnabled = false;
        this.gizmoManager.rotationGizmoEnabled = false;
        this.gizmoManager.scaleGizmoEnabled = false;
        this.currentGizmo = null;
    }

    setGizmoPos() {
        if (this.currentGizmo == "position") {
            this.resetGizmo();
            return;
        }

        this.gizmoManager.positionGizmoEnabled = true;
        this.gizmoManager.rotationGizmoEnabled = false;
        this.gizmoManager.scaleGizmoEnabled = false;
        this.currentGizmo = "position";
    }

    setGizmoRot() {
        if (this.currentGizmo == "rotation") {
            this.resetGizmo();
            return;
        }

        this.gizmoManager.positionGizmoEnabled = false;
        this.gizmoManager.rotationGizmoEnabled = true;
        this.gizmoManager.scaleGizmoEnabled = false;
        this.currentGizmo = "rotation";
    }

    setGizmoScale() {
        if (this.currentGizmo == "scale") {
            this.resetGizmo();
            return;

        }
        this.gizmoManager.positionGizmoEnabled = false;
        this.gizmoManager.rotationGizmoEnabled = false;
        this.gizmoManager.scaleGizmoEnabled = true;
        this.currentGizmo = "scale";
    }

    setFocusToCenter() {
        this.camera.setTarget(this.getSceneCenter());
    }

    setFocusToSelected() {
        if(this.selected) this.camera.setTarget(this.selected.position.clone());
    }

    getSceneCenter() {
        if (this.room) return this.room.getBoundingInfo().boundingBox.centerWorld.clone();
        return BABYLON.Vector3.Zero();
    }
}

export function getScene() {
    let scene = new RoomScene($("#renderer").get(0));
    addDotnetMutators(scene);
    return scene;
}