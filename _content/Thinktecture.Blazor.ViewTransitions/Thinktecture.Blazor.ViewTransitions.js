export function startViewTransition(component, methodName) {
    document.startViewTransition(() => component.invokeMethodAsync(methodName));
}

export function isSupported() {
    return !!document.startViewTransition;
}
