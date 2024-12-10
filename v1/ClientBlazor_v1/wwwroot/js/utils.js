export function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}

export function getNestedObject(object, path){
    path = path.split(".");
    for(let i = 0; i < path.length - 1; i++) object = object[path[i]];
    return [object, path[path.length-1]];
}

export function addSizeProps(mesh){
    mesh.size = {
        get x() {
            return (mesh.getBoundingInfo().boundingBox.extendSize.x * 2) * mesh.scaling.x;
        },
        get y() {
            return (mesh.getBoundingInfo().boundingBox.extendSize.y * 2) * mesh.scaling.y;
        },
        get z() {
            return (mesh.getBoundingInfo().boundingBox.extendSize.z * 2) * mesh.scaling.z;
        },

        set x(value) {
            mesh.scaling.x = value / (mesh.getBoundingInfo().boundingBox.extendSize.x * 2);
        },
        set y(value) {
            mesh.scaling.y = value / (mesh.getBoundingInfo().boundingBox.extendSize.y * 2);
        },
        set z(value) {
            mesh.scaling.z = value / (mesh.getBoundingInfo().boundingBox.extendSize.z * 2);
        },
    };
}