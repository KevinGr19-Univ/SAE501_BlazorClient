import { clamp } from './utils.js';
import { dotnetProxify, addDotnetMutators } from './interop.js';

class RoomScene {

    constructor(canvas) {
        this.canvas = canvas;
        this.engine = new BABYLON.Engine(this.canvas, true, { stencil: true });

        this.scene = new BABYLON.Scene(this.engine);
        this.scene.useRightHandedSystem = true;
        this.scene.clearColor = new BABYLON.Color3(0.3, 0.35, 0.4);

        this.camera = new BABYLON.ArcRotateCamera("camera1", 90, 45, 10, BABYLON.Vector3.Zero(), this.scene);
        this.camera.allowUpsideDown = false;
        this.camera.minZ = 0.01;
        this.camera.setPosition(BABYLON.Vector3.One().scale(10));
        this.camera.attachControl(this.canvas, true);
        this.camera.wheelPrecision = 30;

        this.ambientLight = new BABYLON.HemisphericLight("ambientLight", new BABYLON.Vector3(0, 0, 0), this.scene);
        this.pointLight = new BABYLON.PointLight("pointLight", new BABYLON.Vector3(0, 3, 3), this.scene);
        this.ambientLight.intensity = 0.7;
        this.pointLight.intensity = 0.7;

        this.initMeshes();
        this.initGizmo();

        this.engine.runRenderLoop(() => this.scene.render());
        window.addEventListener('resize', () => this.engine.resize());
    }

    updateRoomMesh(height) {
        if (this.room) {
            this.room.remove();
        }

        this.room = BABYLON.MeshBuilder.CreateBox("room", { width: 6, height: height, depth: 6 }, this.scene);
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
            if (e.key == 'g') {
                this.gizmoManager.positionGizmoEnabled = true;
                this.gizmoManager.rotationGizmoEnabled = false;
                this.gizmoManager.scaleGizmoEnabled = false;
            }

            else if (e.key == 'r') {
                this.gizmoManager.positionGizmoEnabled = false;
                this.gizmoManager.rotationGizmoEnabled = true;
                this.gizmoManager.scaleGizmoEnabled = false;
            }

            else if (e.key == 's') {
                this.gizmoManager.positionGizmoEnabled = false;
                this.gizmoManager.rotationGizmoEnabled = false;
                this.gizmoManager.scaleGizmoEnabled = true;
            }
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
        if (this.selected) {
            this.selected.material.emissiveColor = BABYLON.Color3.Black();
        }

        this.selected = mesh;
        if (this.selected) {
            this.selected.material.emissiveColor = new BABYLON.Color3(0.3, 0.3, 0.3);
        }

        this.gizmoManager.positionGizmoEnabled = false;
        this.gizmoManager.rotationGizmoEnabled = false;
        this.gizmoManager.scaleGizmoEnabled = false;
    }

    addDoor() {
        let door = BABYLON.MeshBuilder.CreateBox("door", { width: 1, height: 2, depth: 0.15 });
        door.material = new BABYLON.StandardMaterial("doorMat");
        door.material.diffuseColor = new BABYLON.Color3(1, 1, 0);

        this.gizmoManager.attachableMeshes.push(door);
        door = dotnetProxify(door, {
            position: {
                x: true,
                y: true,
                z: true
            },
            rotation: {
                y: true
            },
            scaling: {
                x: true,
                y: true,
                z: true
            }
        });

        return door;
    }

}

export function getScene() {
    let scene = new RoomScene($("#renderer").get(0));
    addDotnetMutators(scene);
    return scene;
}