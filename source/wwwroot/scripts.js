export function requestPermission() {
    return new Promise((resolve, reject) => {
        Notification.requestPermission((permission) => {
            resolve(permission);
        });
    });
}

export function isSupported() {
    if ("Notification" in window)
        return true;
    return false;
}

export function create(title, options) {
    return new Notification(title, options);
}