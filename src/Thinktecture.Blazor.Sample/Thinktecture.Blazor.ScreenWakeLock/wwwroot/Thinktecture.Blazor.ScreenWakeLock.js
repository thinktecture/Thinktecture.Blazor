export function isSupported() {
    return isSupportedInWindow() || isSupportedInNavigator();
}

export async function enterFullScreen() {
    await document.documentElement.requestFullscreen();
}

export async function exitFullScreen() {
    await document.exitFullscreen();
}

let wakeLock = null;

export async function requestWakeLock() {
    if (isSupportedInWindow()) {
        requestWakeLockWindow();
    } else if (isSupportedInNavigator()) {
        await requestWakeLockNavigator();
    }
}

export function releaseWakeLock() {
    if (wakeLock) {
        wakeLock.release();
        wakeLock = null;
    }
}

async function requestWakeLockNavigator() {
    const requestWakeLock = async () => {
        try {
            wakeLock = await navigator.wakeLock.request('screen');
            wakeLock.addEventListener('release', (e) => {
                console.log(e);
                wakeLockCheckbox.checked = false;
                statusDiv.textContent = 'Wake Lock was released';
                console.log('Wake Lock was released');
            });
            wakeLockCheckbox.checked = true;
            statusDiv.textContent = 'Wake Lock is active';
            console.log('Wake Lock is active');
        } catch (e) {
            wakeLockCheckbox.checked = false;
            statusDiv.textContent = `${e.name}, ${e.message}`;
            console.error(`${e.name}, ${e.message}`);
        }
    };
    
    releaseWakeLock();

    await requestWakeLock();
}

function requestWakeLockWindow() {
    const requestWakeLock = () => {
        const controller = new AbortController();
        const signal = controller.signal;
        window.WakeLock.request('screen', {signal})
            .catch((e) => {
                if (e.name === 'AbortError') {
                    console.log('Wake Lock was aborted');
                } else {
                    console.error(`${e.name}, ${e.message}`);
                }
            });
        console.log('Wake Lock is active');
        return controller;
    };
    
    if (wakeLock) {
        releaseWakeLock();
    }

    wakeLock = requestWakeLock();
}

function isSupportedInNavigator() {
    return 'wakeLock' in navigator && 'request' in navigator.wakeLock;
}

function isSupportedInWindow() {
    return 'WakeLock' in window && 'request' in window.WakeLock;
}