export function writeText(data) {
    return navigator.clipboard.writeText(data);
}

export function write(data) {
    // TODO: Chromium/Safari promise problem
    // https://bugs.webkit.org/show_bug.cgi?id=222262#c5
    return navigator.clipboard.write(data);
}

export function readText() {
    return navigator.clipboard.readText();
}

export function read() {
    return navigator.clipboard.read();
}

export function isSupported() {
    return 'clipboard' in navigator && 'write' in navigator.clipboard;
}

export function getClipboardItems(clipboardItems) {
    return clipboardItems.map(({items, options}) => new ClipboardItem(items, options));
}

export function getArrayLength(array) {
    return array.length;
}
