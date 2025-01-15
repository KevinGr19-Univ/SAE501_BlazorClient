let models = {};

async function loadModel(path, filename, scene) {
    //let imported = await BABYLON.SceneLoader.AppendAsync(path, undefined, undefined, (event) => { }, ext);
    //let model = imported.getNodeByName("__root__").getChildMeshes()[0];
    let imported = await BABYLON.SceneLoader.ImportMeshAsync("", path, filename, scene);
    let model = imported.meshes[0];
    model.setParent(null);
    //model.setEnabled(false);
    return model;
}

export async function startLoadingModels(scene) {
    models = {
        door: loadModel("/3d/_models/", "door.glb", scene),
        //table: loadModel("/3d/models/table.glb", "glb"),
        //window: loadModel("/3d/models/window.glb", "glb"),
        //heater: loadModel("/3d/models/heater.glb", "glb"),
        //sensor6in1: loadModel("/3d/models/sensor6in1.glb", "glb"),
        //sensor9in1: loadModel("/3d/models/sensor9in1.glb", "glb"),
        //sensorCO2: loadModel("/3d/models/sensorCO2.glb", "glb"),
        //lamp: loadModel("/3d/models/lamp.glb", "glb"),
        //plug: loadModel("/3d/models/plug.glb", "glb"),
        //siren: loadModel("/3d/models/siren.glb", "glb"),
    };
}

async function createModel(modelKey) {
    let original = await models[modelKey];
    let clone = original.clone();
    clone.setEnabled(true);
    return clone;
}

export var objectInfos = {
    door: {
        meshBuilder: async (roomScene) => {
            //let door = BABYLON.MeshBuilder.CreateBox("door", { width: 1, height: 2, depth: 0.15 }, roomScene.scene);
            //door.material = new BABYLON.StandardMaterial("doorMat");
            //door.material.diffuseColor = new BABYLON.Color3(0.5, 0.5, 0);
            //return door;
            return await createModel("door");
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
    table: {
        meshBuilder: async (roomScene) => {
            let table = BABYLON.MeshBuilder.CreateBox("table", { width: 2, height: 1, depth: 1.2 }, roomScene.scene);
            table.material = new BABYLON.StandardMaterial("tableMat");
            table.material.diffuseColor = new BABYLON.Color3(0, 0, 0.5);
            return table;
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
    window: {
        meshBuilder: async (roomScene) => {
            let window = BABYLON.MeshBuilder.CreateBox("window", { width: 1, height: 1.4, depth: 0.15 }, roomScene.scene);
            window.material = new BABYLON.StandardMaterial("windowMat");
            window.material.diffuseColor = new BABYLON.Color3(0, 0.5, 0.5);
            return window;
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
    heater: {
        meshBuilder: async (roomScene) => {
            let heater = BABYLON.MeshBuilder.CreateBox("heater", { width: 1.6, height: 0.8, depth: 0.4 }, roomScene.scene);
            heater.material = new BABYLON.StandardMaterial("doorMat");
            heater.material.diffuseColor = new BABYLON.Color3(0.5, 0, 0);
            return heater;
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
    siren: {
        meshBuilder: async (roomScene) => {
            let siren = BABYLON.MeshBuilder.CreateBox("siren", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            siren.material = new BABYLON.StandardMaterial("sirenMat");
            siren.material.diffuseColor = new BABYLON.Color3(1, 0, 1);
            return siren;
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
            }
        }
    },
    plug: {
        meshBuilder: async (roomScene) => {
            let plug = BABYLON.MeshBuilder.CreateBox("plug", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            plug.material = new BABYLON.StandardMaterial("plugMat");
            plug.material.diffuseColor = new BABYLON.Color3(1, 1, 0);
            return plug;
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
            }
        }
    },
    lamp: {
        meshBuilder: async (roomScene) => {
            let sensor = BABYLON.MeshBuilder.CreateBox("lamp", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            sensor.material = new BABYLON.StandardMaterial("lampMat");
            sensor.material.diffuseColor = new BABYLON.Color3(1, 0.5, 0);
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
    sensor6in1: {
        meshBuilder: async (roomScene) => {
            let sensor6in1 = BABYLON.MeshBuilder.CreateBox("sensor6in1", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            sensor6in1.material = new BABYLON.StandardMaterial("sensor6in1Mat");
            sensor6in1.material.diffuseColor = new BABYLON.Color3(0, 0, 1);
            return sensor6in1;
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
            }
        }
    },
    sensor9in1: {
        meshBuilder: async (roomScene) => {
            let sensor9in1 = BABYLON.MeshBuilder.CreateBox("sensor9in1", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            sensor9in1.material = new BABYLON.StandardMaterial("sensor9in1Mat");
            sensor9in1.material.diffuseColor = new BABYLON.Color3(0, 1, 0);
            return sensor9in1;
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
            }
        }
    },
    sensorCO2: {
        meshBuilder: async (roomScene) => {
            let sensorCO2 = BABYLON.MeshBuilder.CreateBox("sensorCO2", { width: 0.3, height: 0.3, depth: 0.3 }, roomScene.scene);
            sensorCO2.material = new BABYLON.StandardMaterial("sensorCO2Mat");
            sensorCO2.material.diffuseColor = new BABYLON.Color3(1, 0, 0);
            return sensorCO2;
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
            }
        }
    },
};