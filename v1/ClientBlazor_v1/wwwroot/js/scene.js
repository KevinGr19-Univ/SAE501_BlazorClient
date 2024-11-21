function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}

function getNestedObject(object, path){
    path = path.split(".");
    for(let i = 0; i < path.length - 1; i++) object = object[path[i]];
    return object, path[path.length-1];
}

class Scene {

    constructor(canvas) {
        this.canvas = canvas;
        this.engine = new BABYLON.Engine(this.canvas, true);

        this.scene = new BABYLON.Scene(this.engine);
        this.scene.clearColor = new BABYLON.Color3(0.3, 0.35, 0.4);

        this.camera = new BABYLON.ArcRotateCamera("camera1", 90, 45, 10, BABYLON.Vector3.Zero(), this.scene);
        this.camera.allowUpsideDown = false;
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
        this.gizmoManager.clearGizmoOnEmptyPointerEvent = true;
        this.gizmoManager.updateGizmoRotationToMatchAttachedMesh = false;

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

    clickAddCapteur(dotnetRef) {
        let capteur = BABYLON.MeshBuilder.CreateBox("capteur", { width: 1, height: 1, depth: 1 }, this.scene);
        capteur.material = new BABYLON.StandardMaterial(capteur.name + "_mat");
        capteur.material.diffuseColor = new BABYLON.Color3(1, 0, 0);

        this.gizmoManager.attachableMeshes.push(capteur);

        capteur = this.dotnetSceneObjectProxy(capteur, dotnetRef);
        return capteur;
    }

    dotnetSceneObjectProxy(object, dotnetRef){
        object.dotnetGet = function(prop){
            let [object, lastProp] = getNestedObject(this, prop);
            return object[lastProp];
        }

        object.dotnetSet = function(prop, value){
            let [object, lastProp] = getNestedObject(this, prop);
            object[lastProp] = value;
        }

        return new Proxy(object, {
            set(target, prop, value){
                let dotnetPrefix = null;
                if(prop == "position") dotnetPrefix = "Pos"
                else if(prop == "rotation") dotnetPrefix = "Rot";
                else if(prop == "scale") dotnetPrefix = "Scale";

                if(dotnetPrefix)
                    value = dotnetVectorProxy(value, dotnetPrefix, dotnetRef);

                return Reflect.set(...arguments);
            }
        });
    }

    dotnetVectorProxy(vector, prefix, dotnetRef){
        return new Proxy(vector, {
            set(target, prop, value){
                if(!Reflect.set(...arguments)) return false;

                if(prop == "x" || prop == "y" || prop == "z")
                    dotnetRef.invokeMethod("OnPropertyChanged", `${prefix}${prop.toUpperCase()}`);
                return true;
            }
        });
    }

}