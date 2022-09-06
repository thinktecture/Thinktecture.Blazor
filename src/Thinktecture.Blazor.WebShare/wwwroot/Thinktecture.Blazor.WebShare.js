export async function isSupported(basic) {
    return !!basic
        ? 'share' in navigator
        : 'share' in navigator && 'canShare' in navigator;
}

export function canShare(data) {
    return navigator.canShare(data);
}

export function share(data) {
    return navigator.share(data);
}