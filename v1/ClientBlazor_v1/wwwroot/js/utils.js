export function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}

export function getNestedObject(object, path){
    path = path.split(".");
    for(let i = 0; i < path.length - 1; i++) object = object[path[i]];
    return [object, path[path.length-1]];
}

export function addSizeProps(node){
    node.size = {
        get x() {
            return (node.getBoundingInfo().boundingBox.extendSize.x * 2) * node.scaling.x;
        },
        get y() {
            return (node.getBoundingInfo().boundingBox.extendSize.y * 2) * node.scaling.y;
        },
        get z() {
            return (node.getBoundingInfo().boundingBox.extendSize.z * 2) * node.scaling.z;
        },

        set x(value) {
            node.scaling.x = value / (node.getBoundingInfo().boundingBox.extendSize.x * 2);
        },
        set y(value) {
            node.scaling.y = value / (node.getBoundingInfo().boundingBox.extendSize.y * 2);
        },
        set z(value) {
            node.scaling.z = value / (node.getBoundingInfo().boundingBox.extendSize.z * 2);
        },
    };
}

export function isClockwiseXZ(points) {
    let twiceEnclosedArea = 0;
    for (let i = 0; i < points.length; i++) {
        let p1 = points[i];
        let p2 = points[(i + 1) % points.length];
        twiceEnclosedArea += (p2.x - p1.x) * (p2.z + p1.z);
    }
    return twiceEnclosedArea >= 0;
}