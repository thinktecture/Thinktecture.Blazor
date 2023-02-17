const installedStateKey = 'installed';

export async function registerUpdateEvent(caller, methodName) {
    const registration = await navigator.serviceWorker.ready;
    registration.onupdatefound = () => {
        const installingServiceWorker = registration.installing;
        installingServiceWorker.onstatechange = () => {
            console.log(installingServiceWorker.state);
            if (installingServiceWorker.state === installedStateKey) {
                caller.invokeMethodAsync(methodName).catch(err => console.log(err));
            }
        }
    }
}

export function reload() {
    window.location.reload();
}