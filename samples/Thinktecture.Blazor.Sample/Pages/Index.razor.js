export function generateSampleFile() {
    const blob = new Blob(["foo"], { type: 'text/plain' });
    return new File([blob], 'text.txt');
}