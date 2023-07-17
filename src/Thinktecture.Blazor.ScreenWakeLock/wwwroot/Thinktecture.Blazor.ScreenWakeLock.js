let wakeLock = null;

export async function requestWakeLock(component, method) {
    if (isSupported()) {
        await requestWakeLockNavigator(component, method);
    }
}

export function isSupported() {
    return 'wakeLock' in navigator && 'request' in navigator.wakeLock;
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