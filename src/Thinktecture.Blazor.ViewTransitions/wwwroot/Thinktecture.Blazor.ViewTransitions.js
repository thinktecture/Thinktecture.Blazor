export function startViewTransition(component, methodName) {
    if (!document.startViewTransition) {
        component.invokeMethodAsync(methodName);
        return;
    }
    // With a transition:
    document.startViewTransition(() => component.invokeMethodAsync(methodName));
}

export function isSupported() {
    return !!document.startViewTransition;
}
