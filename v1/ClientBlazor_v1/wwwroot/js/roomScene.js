﻿import { clamp, addSizeProps } from './utils.js';
import { dotnetProxify, addDotnetMutators } from './interop.js';
import { objectInfos } from './objectInfos.js';
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
        this.ambientLight.intensity = 0.7;
        this.pointLight.intensity = 0.7;

        this.initMeshes();
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
    }

    updateRoomMesh(points, height) {
        if (this.room) {
            this.room.remove();
        }

        this.room = BABYLON.MeshBuilder.ExtrudePolygon("room", {
            shape: points.map(p => new BABYLON.Vector3(p.x, 0, p.y)),
            depth: height
        }, this.scene, earcut);
        this.room.rotation.x = Math.PI;
        
        let translation = this.room.getBoundingInfo().boundingBox.center.scale(-1);
        this.room.setPivotMatrix(BABYLON.Matrix.Translation(translation.x, translation.y, translation.z), false);

        this.room.material = new BABYLON.StandardMaterial("roomMat");
        this.room.flipFaces(true);
        this.room.isPickable = false;
    }

    initMeshes() {
        this.templates = {};
        BABYLON.SceneLoader.ShowLoadingScreen = false;

        const loadTemplate = (key, modelPath, ext) => {
            let meshLoading = BABYLON.SceneLoader.AppendAsync(modelPath, undefined, this.scene, (event) => { }, ext);
            return meshLoading.then((scene) => {
                this.templates[key] = scene.getNodeByName("__root__").getChildMeshes()[0];
                this.templates[key].setParent(null);
                this.templates[key].setEnabled(false);
            });
        };
    }

    initGizmo() {
        this.gizmoManager = new BABYLON.GizmoManager(this.scene);

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
        if (this.selected == mesh) return;

        if (this.selected) {
            this.selected.material.emissiveColor = BABYLON.Color3.Black();
        }

        this.selected = mesh;
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
        else {
            this.gizmoManager.attachToMesh(null);
        }
    }

    addDoor() {
        return this._addRoomObject("door");
    }

    addSensor() {
        return this._addRoomObject("sensor");
    }

    _addRoomObject(key) {
        let mesh = objectInfos[key].meshBuilder(this);
        this.gizmoManager.attachableMeshes.push(mesh);

        addSizeProps(mesh);
        mesh.roomObjectKey = key;
        mesh = dotnetProxify(mesh, objectInfos[key].bindedProps);

        mesh.sceneSelect = () => this.setSelected(mesh);
        mesh.sceneUnselect = () => {
            if (this.selected === mesh) this.setSelected(null);
        }
        return mesh;
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
        this.camera.setTarget(BABYLON.Vector3.Zero());
    }

    setFocusToSelected() {
        if(this.selected) this.camera.setTarget(this.selected.position.clone());
    }

}

export function getScene() {
    let scene = new RoomScene($("#renderer").get(0));
    addDotnetMutators(scene);
    return scene;
}