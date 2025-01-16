let loadPromise = null;

export async function startLoadingModels(scene) {
    loadPromise = (async () => {
        let imported = await BABYLON.SceneLoader.ImportMeshAsync("", "/3d/_models/", "roomobjects.glb", scene);
        let root = imported.meshes[0];
        root.setParent(null);
        root.setEnabled(false);
    })();
    await loadPromise;
}

async function createModel(modelKey, scene) {
    await loadPromise;
    let original = scene.getMeshByName(`r_${modelKey}`);
    let clone = original.clone();
    clone.setParent(null);
    clone.setEnabled(true);

    clone.material = new BABYLON.StandardMaterial("hitbox");
    clone.renderingGroupId = 1;
    return clone;
}

export var objectInfos = {
    door: {
        meshBuilder: async (scene) => await createModel("door", scene),
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
        meshBuilder: async (scene) => await createModel("table", scene),
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
        meshBuilder: async (scene) => await createModel("window", scene),
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
        meshBuilder: async (scene) => await createModel("heater", scene),
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
        meshBuilder: async (scene) => await createModel("siren", scene),
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
        meshBuilder: async (scene) => await createModel("plug", scene),
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
        meshBuilder: async (scene) => await createModel("lamp", scene),
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
        meshBuilder: async (scene) => await createModel("sensor6in1", scene),
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
        meshBuilder: async (scene) => await createModel("sensor9in1", scene),
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
        meshBuilder: async (scene) => await createModel("sensorCO2", scene),
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
    customObject: {
        meshBuilder: async (scene) => {
            let obj = await createModel("customobject", scene);
            obj.colorMesh = obj.getChildMeshes()[0];
            obj.colorMesh.material = new BABYLON.StandardMaterial("customObject_colorMat");

            obj.setCustomObjectColor = (color) => {
                let babylonColor = new BABYLON.Color3(
                    ((color >> 16) & 0xFF) / 256,
                    ((color >> 8) & 0xFF) / 256,
                    (color & 0xFF) / 256,
                );
                obj.colorMesh.material.diffuseColor = babylonColor;
            };
            return obj;
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
            size: {
                x: true,
                y: true,
                z: true
            }
        }
    }
};