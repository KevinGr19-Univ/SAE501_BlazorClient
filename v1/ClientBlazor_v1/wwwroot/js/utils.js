export function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}

export function getNestedObject(object, path){
    path = path.split(".");
    for(let i = 0; i < path.length - 1; i++) object = object[path[i]];
    return [object, path[path.length-1]];
}