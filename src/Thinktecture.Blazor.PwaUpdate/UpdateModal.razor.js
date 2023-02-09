const installedStateKey = 'installed';
const serviceWorkerPropertyKey = 'serviceWorker';

/*
 * updateFound ok aber für hier nicht ok
 * services ready => registration => handle event
 * 
 **/
export async function registerUpdateEvent(caller, methodName) {
    const registration = await navigator.serviceWorker.ready;
    registration.onupdatefound = () => {
        const installingServiceWorker = registration.installing;
        installingServiceWorker.onstatechange = () => {
            console.log(installingServiceWorker.state);
            if (installingServiceWorker.state === installedStateKey) {
                caller.invokeMethodAsync(methodName).then();
            }
        }
    }
}