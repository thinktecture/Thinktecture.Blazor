export async function canShare(data) {
    return navigator.canShare(data);
}

export async function share(data) {
    await navigator.share(data);
}