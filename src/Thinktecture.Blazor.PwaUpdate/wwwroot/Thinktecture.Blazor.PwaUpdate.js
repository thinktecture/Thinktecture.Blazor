export async function registerUpdateEvent(caller, methodName) {
    const registration = await navigator.serviceWorker.ready;
    registration.onupdatefound = async () => {
        const installingServiceWorker = registration.installing;
        installingServiceWorker.onstatechange = () => {
            if (installingServiceWorker.state === 'installed') {
                await caller.invokeMethodAsync(methodName);
            }
        }
    }
}

export function reload() {
    window.location.reload();
}