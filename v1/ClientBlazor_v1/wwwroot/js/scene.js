import { clamp } from './utils.js';
import { dotnetProxify, addDotnetMutators } from './interop.js';

class Scene {

    constructor(canvas) {
        this.canvas = canvas;
        this.engine = new BABYLON.Engine(this.canvas, true, { stencil: true });

        this.scene = new BABYLON.Scene(this.engine);
        this.scene.clearColor = new BABYLON.Color3(0.3, 0.35, 0.4);

        this.camera = new BABYLON.ArcRotateCamera("camera1", 90, 45, 10, BABYLON.Vector3.Zero(), this.scene);
        this.camera.allowUpsideDown = false;
        this.camera.minZ = 0.01;
        this.camera.setPosition(BABYLON.Vector3.One().scale(10));
        this.camera.attachControl(this.canvas, true);

        this.light1 = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(0, 0, 0), this.scene);
        this.light2 = new BABYLON.PointLight("light2", new BABYLON.Vector3(0, 3, 3), this.scene);
        this.light1.intensity = 0.7;
        this.light2.intensity = 0.7;

        this.initMeshes();
        this.initGizmo();

        this.engine.runRenderLoop(() => this.scene.render());
        window.addEventListener('resize', () => this.engine.resize());
    }

    initMeshes() {
        this.room = BABYLON.MeshBuilder.CreateBox("room", { width: 10, height: 3, depth: 8 }, this.scene);
        this.room.material = new BABYLON.StandardMaterial("ground");
        this.room.flipFaces(true);
        this.room.isPickable = false;
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
            else if(!mesh) {
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

            for (let dim of ["x", "y", "z"]){
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
            console.log(this.selected.onUnselect);
            if (this.selected.onUnselect) this.selected.onUnselect();
        }

        this.selected = mesh;
        if (this.selected) {
            this.selected.material.emissiveColor = new BABYLON.Color3(0.3, 0.3, 0.3);
            if(this.selected.onSelect) this.selected.onSelect();
        }

        this.gizmoManager.positionGizmoEnabled = false;
        this.gizmoManager.rotationGizmoEnabled = false;
        this.gizmoManager.scaleGizmoEnabled = false;
    }

    addSensor() {
        let sensor = BABYLON.MeshBuilder.CreateBox("sensor", { width: 1, height: 1, depth: 1 }, this.scene);
        //sensor.scaling = new BABYLON.Vector3(0.06, 0.04, 0.06);
        sensor.material = new BABYLON.StandardMaterial(sensor.name + "_mat");
        sensor.material.diffuseColor = new BABYLON.Color3(1, 0, 0);
        
        this.gizmoManager.attachableMeshes.push(sensor);
        sensor = this.dotnetProxifyTransformFull(sensor);

        console.log("Test");
        return sensor;
    }

    dotnetProxifyTransformFull(object) {
        return dotnetProxify(object, {
            position: {
                x: 'PosX',
                y: 'PosY',
                z: 'PosZ',
            },
            rotation: {
                x: 'RotX',
                y: 'RotY',
                z: 'RotZ',
            },
            scaling: {
                x: 'ScaleX',
                y: 'ScaleY',
                z: 'ScaleZ',
            }
        })
    }

}

export function getScene(){
    let scene = new Scene($("#renderer").get(0));
    addDotnetMutators(scene);
    return scene;
}