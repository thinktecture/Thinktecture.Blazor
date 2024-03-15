export async function registerUpdateEvent(caller, methodName) {
    const registration = await navigator.serviceWorker.ready;
    registration.onupdatefound = () => {
        const installingServiceWorker = registration.installing;
        installingServiceWorker.onstatechange = () => {
            if (installingServiceWorker.state.startsWith('installed')) {
                caller.invokeMethodAsync(methodName);
            }
        }
    }
}

export function reload() {
    window.location.reload();
}