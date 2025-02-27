function nestedProp(object, propName, optionalNewValue) {
    let path = propName.split(".");
    for (let i = 0; i < path.length - 1; i++) object = object[path[i]];

    if (optionalNewValue === undefined) return object[path[path.length - 1]];
    return object[path[path.length - 1]] = optionalNewValue;
}

export function addDotnetMutators(object) {
    object.dotnetGet = function (propName) {
        return nestedProp(this, propName);
    }

    object.dotnetSet = function (propName, value) {
        nestedProp(this, propName, value);
    }
};

export function dotnetProxify(object, notifiedProps) {
    addDotnetMutators(object);

    const notifyProp = (dotnetRef, notifiedProp) => {
        if (notifiedProp) {
            dotnetRef.invokeMethod("RequireUIUpdate");
        }
    };

    const createProxy = (root, object, notifiedProps) => {
        return new Proxy(object, {
            get(target, prop) {
                if (prop == "__isProxy") return true;

                let subProps = notifiedProps[prop];
                let value = Reflect.get(...arguments);

                if (value && value instanceof Object && subProps && subProps instanceof Object)
                    return createProxy(root, value, subProps);

                return value;
            },

            set(target, prop, newValue) {
                if (!Reflect.set(...arguments)) return false;

                notifyProp(root.dotnetRef, notifiedProps[prop]);
                return true;
            }
        });
    };

    object = createProxy(object, object, notifiedProps);
    object.proxy = object;

    return object;
}