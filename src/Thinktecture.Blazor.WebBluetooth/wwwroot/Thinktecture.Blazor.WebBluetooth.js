export async function isSupported() {
    if ('bluetooth' in navigator) {
        return navigator.bluetooth.getAvailability();
    }
    return false;
}

export async function getDevice(name) {
    try {
        const result = await navigator.bluetooth.requestDevice({filters:[ {name: name} ]});
        return {
            id: result.id,
            name: result.name,
            gatt: result.gatt
        }
    }
    catch(e) {
        console.log(e);
        return null;
    }
}

export async function getName(device) {
    return device.name;
}


export async function getId(device) {
    return device.id;
}

export async function getConnected(device) {
    return device.gatt.connected;
}