import { getNestedObject } from "./utils.js";

let listenedVectors = {
    "position": "Pos",
    "rotation": "Rot",
    "scaling": "Scale"
};

let axes = ["x", "y", "z"];

export function dotnetTransformProxy(object, dotnetRef){
    object.dotnetGet = function(prop){
        let [object, lastProp] = getNestedObject(this, prop);
        return object[lastProp];
    }

    object.dotnetSet = function(prop, value){
        let [object, lastProp] = getNestedObject(this, prop);
        object[lastProp] = value;
    }

    object = new Proxy(object, {
        get(target, prop){
            if(prop == "__isProxy") return true;

            let dotnetPrefix = listenedVectors[prop];
            let value = Reflect.get(...arguments);

            if(dotnetPrefix)
                value = dotnetVectorProxy(value, dotnetPrefix, dotnetRef);

            return value;
        },

        set(target, prop, value){
            if(!Reflect.set(...arguments)) return false;
            let dotnetPrefix = listenedVectors[prop];

            if(dotnetPrefix){
                for(let axis in axes)
                    dotnetRef.invokeMethod("OnPropertyChanged", `${dotnetPrefix}${axis.toUpperCase()}`);
            }
            return true;
        }
    });

    object.proxy = object;
    return object;
}

function dotnetVectorProxy(vector, prefix, dotnetRef){
    return new Proxy(vector, {
        set(target, prop, value){
            if(!Reflect.set(...arguments)) return false;

            if(axes.includes(prop))
                dotnetRef.invokeMethod("OnPropertyChanged", `${prefix}${prop.toUpperCase()}`);
            return true;
        }
    });
}