export async function isSupported(basic) {
    return !!basic
        ? 'share' in navigator
        : 'share' in navigator && 'canShare' in navigator;
}

export async function canShare(data) {
    if (data.files) {
        return navigator.canShare(data);
    } else {
        const items = { title: data.title, text: data.text, url: data.url }
        return navigator.canShare(items);
    }
}

export async function share(data) {
    await navigator.share(data);
}