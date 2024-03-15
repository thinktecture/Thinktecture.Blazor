export function isSupported() {
    return 'setAppBadge' in navigator && 'clearAppBadge' in navigator;
}

export function setAppBadge(contents) {
    return navigator.setAppBadge(contents ?? undefined);
}

export function clearAppBadge() {
    return navigator.clearAppBadge();
}
