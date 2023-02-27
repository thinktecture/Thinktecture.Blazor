namespace Thinktecture.Blazor.Notification.Models {
    public class Notification<T> {
        /// <summary>
        /// The title that must be shown within the notification.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// An object that allows configuring the notification.
        /// </summary>
        public NotificationOption<T>? Options { get; set; }
    }
    
    public class NotificationOption<T> {

        /// <summary>
        /// An array of actions to display in the notification.
        /// </summary>
        public NotificationAction[] Actions { get; set; } = Array.Empty<NotificationAction>();

        /// <summary>
        /// a string containing the URL of an image to represent the notification when there is not enough space to display 
        /// the notification itself such as for example, the Android Notification Bar. On Android devices, 
        /// the badge should accommodate devices up to 4x resolution, about 96 by 96 px, 
        /// and the image will be automatically masked.
        /// </summary>
        public string Badge { get; set; } = string.Empty;

        /// <summary>
        /// A string representing an extra content to display within the notification.
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Arbitrary data that you want to be associated with the notification. 
        /// This can be of any data type.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// The direction of the notification; it can be auto, ltr or rtl.
        /// </summary>
        public string Dir { get; set; } = string.Empty;

        /// <summary>
        /// A string containing the URL of an image to be used as an icon by the notification.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// A string containing the URL of an image to be displayed in the notification.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Specify the lang used within the notification. This string must be a valid language.
        /// </summary>
        public string Lang { get; set; } = string.Empty;

        /// <summary>
        /// A boolean that indicates whether to suppress vibrations and audible alerts 
        /// when reusing a tag value. If options's renotify is true and options's tag 
        /// is the empty string a TypeError will be thrown. 
        /// The default is false.
        /// </summary>
        public bool Renotify { get; set; } = false;

        /// <summary>
        /// Indicates that on devices with sufficiently large screens, 
        /// a notification should remain active until the user clicks or dismisses it. 
        /// If this value is absent or false, the desktop version of Chrome will auto-minimize 
        /// notifications after approximately twenty seconds. 
        /// The default value is false.
        /// </summary>
        public bool RequireInteraction { get; set; } = false;

        /// <summary>
        /// When set indicates that no sounds or vibrations should be made. 
        /// If options's silent is true and options's vibrate is present a TypeError exception will be thrown. 
        /// The default value is false.
        /// </summary>
        public bool Silent { get; set; } = false;

        /// <summary>
        /// An ID for a given notification that allows you to find, replace, or remove the notification using a script if necessary.
        /// </summary>
        public string Tag { get; set; } = string.Empty;

        /// <summary>
        /// A vibration pattern to run with the display of the notification. 
        /// A vibration pattern can be an array with as few as one member. 
        /// The values are times in milliseconds where the even indices (0, 2, 4, etc.) 
        /// indicate how long to vibrate and the odd indices indicate how long to pause. 
        /// For example, [300, 100, 400] would vibrate 300ms, pause 100ms, then vibrate 400ms.
        /// </summary>
        public int[] Vibrate { get; set; } = Array.Empty<int>();
    }

    public class NotificationAction 
    {
        /// <summary>
        /// A string identifying a user action to be displayed on the notification.
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// A string containing action text to be shown to the user.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A string containing the URL of an icon to display with the action.
        /// </summary>
        public string Icon { get; set; } = string.Empty;
    }
}
