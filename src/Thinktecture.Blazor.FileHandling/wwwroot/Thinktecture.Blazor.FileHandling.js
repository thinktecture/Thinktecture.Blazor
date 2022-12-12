let launchParams = null;

export function isSupported() {
    return 'launchQueue' in window;
}

export function setConsumer(component, consumerRef) {
    window.launchQueue.setConsumer(params => {
        launchParams = params;
        component.invokeMethodAsync("InvokeConsumer", consumerRef);
    });
}

// The following three methods are used by the FileHandlingRelay.
// Workaround for https://github.com/dotnet/aspnetcore/issues/26049
export function getTargetUrl() {
    return launchParams.targetUrl ?? null;
}

export function getFiles() {
    const files = launchParams.files;
    launchParams = null;
    return files;
}

export function getArrayLength(array) {
    return array.length;
}
