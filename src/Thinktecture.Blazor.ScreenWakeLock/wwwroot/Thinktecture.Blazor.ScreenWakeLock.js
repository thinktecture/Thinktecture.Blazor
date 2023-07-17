export function isSupported() {
    return isSupportedInWindow() || isSupportedInNavigator();
}

let wakeLock = null;

export async function requestWakeLock(component, method) {
    if (isSupportedInWindow()) {
        requestWakeLockWindow(component, method);
    } else if (isSupportedInNavigator()) {
        await requestWakeLockNavigator(component, method);
    }
}

export function releaseWakeLock() {
    if (wakeLock) {
        wakeLock.release();
        wakeLock = null;
    }
}

async function requestWakeLockNavigator(component, method) {
    const requestWakeLock = async () => {
        try {
            wakeLock = await navigator.wakeLock.request('screen');
            wakeLock.addEventListener('release', (_) => {
                component.invokeMethodAsync(method);
            });
        } catch (e) {
            throw e;
        }
    };
    
    releaseWakeLock();
    await requestWakeLock();
}

function requestWakeLockWindow(component, method) {
    const requestWakeLock = () => {
        const controller = new AbortController();
        const signal = controller.signal;
        window.WakeLock.request('screen', {signal})
            .catch((e) => {
                if (e.name === 'AbortError') {
                    component.invokeMethodAsync(method);
                } else {
                    throw e;
                }
            });
        return controller;
    };
    
    releaseWakeLock();
    wakeLock = requestWakeLock();
}

function isSupportedInNavigator() {
    return 'wakeLock' in navigator && 'request' in navigator.wakeLock;
}

function isSupportedInWindow() {
    return 'WakeLock' in window && 'request' in window.WakeLock;
}