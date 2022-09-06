export async function writeText(data) {
  await navigator.clipboard.writeText(data);
}

export async function write(data) {
  // TODO: Chromium/Safari promise problem
  // https://bugs.webkit.org/show_bug.cgi?id=222262#c5
  await navigator.clipboard.write(data);
}

export async function readText() {
  return await navigator.clipboard.readText();  
}

export async function read() {
  return await navigator.clipboard.read();
}
