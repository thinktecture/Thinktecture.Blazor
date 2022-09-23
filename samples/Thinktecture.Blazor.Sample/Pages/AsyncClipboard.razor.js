export function getBlazorLogoBlobPromise() {
    return new Promise(async (res, rej) => {
        try {
            const logoResponse = await fetch('./icon-192.png');
            res(logoResponse.blob());
        } catch (err) {
            rej(err);
        }
    });
}

export function showBlob(blob) {
    window.open(URL.createObjectURL(blob));
}
