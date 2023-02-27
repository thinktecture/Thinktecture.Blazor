export async function registerNavigationClick(caller, methodName) {
    const registration = await navigator.serviceWorker.ready;
    registration.onnotificationclick = (event) => {
        console.log('Notification clicked');
        event.notification.close();

        event.waitUntil(clients.matchAll({
            type: "window"
        }).then((clientList) => {
            console.log(clientList);
            for (const client of clientList) {
                if (client.url === '/' && 'focus' in client)
                    return client.focus();
            }
            if (clients.openWindow)
                return clients.openWindow('/');
        }));
    }
}

export async function showNotification(title, message) {
    const registration = await navigator.serviceWorker.ready;

    if (Notification.permission === 'granted') {
        await notify(title, message, registration);
    }
    else {
        if (Notification.permission !== 'denied') {
            const permission = await Notification.requestPermission();

            if (permission === 'granted') {
                await notify(title, message, registration);
            }
        }
    }
}

async function notify(title, body, registration) {
    const payload = {
        body
    };

    if ('showNotification' in registration) {
        registration.showNotification(title, payload);
    }
    else {
        new Notification(title, payload);
    }
}