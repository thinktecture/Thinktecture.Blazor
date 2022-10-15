export function isSupported() {
    return 'launchQueue' in window;
}

export function setConsumer(blazorFn) {
    window.launchQueue.setConsumer(params => blazorFn(params));
}
