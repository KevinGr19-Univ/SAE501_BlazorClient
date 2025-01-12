export var objectInfos = {
    door: {
        meshBuilder: (roomScene) => {
            let door = BABYLON.MeshBuilder.CreateBox("door", { width: 1, height: 2, depth: 0.15 }, roomScene.scene);
            door.material = new BABYLON.StandardMaterial("doorMat");
            door.material.diffuseColor = new BABYLON.Color3(1, 1, 0);
            return door;
        },
        bindedProps: {
            position: {
                x: true,
                y: true,
                z: true,
            },
            rotation: {
                y: true
            },
            size: {
                x: true,
                y: true,
                z: true
            }
        }
    },

    lamp: {
        meshBuilder: (roomScene) => {
            let sensor = BABYLON.MeshBuilder.CreateBox("lamp", { width: 0.1, height: 0.1, depth: 0.1 }, roomScene.scene);
            sensor.material = new BABYLON.StandardMaterial("lampMat");
            sensor.material.diffuseColor = new BABYLON.Color3(1, 0, 0);
            return sensor;
        },
        bindedProps: {
            position: {
                x: true,
                y: true,
                z: true,
            },
            rotation: {
                x: true,
                y: true,
                z: true
            },
        }
    },
};