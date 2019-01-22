namespace BrowseSharp.Scripting.Navigator
{
    public interface INavigatorProperties
    {
        // TODO: VRDisplay activeVRDisplays
        string appCodeName { get; }
        // TODO: DOMString appName { get; }
        // TODO: DOMString appVersion { get; }
        // TODO: BatteryManager battery { get; }
        // TODO: NetworkInformation connection { get; }
        bool cookieEnabled { get; }
        // TODO: GeoLocation geolocation { get; }
        // TODO: NavigatorConcurrentHardware NavigatorConcurrentHardware { get; };//NavigatorConcurrentHardware.hardwareConcurrency
        // TODO: NavigatorPlugins NavigatorPlugins { get; }//NavigatorPlugins.javaEnabled
        // TODO: Keyboard keyboard { get; }
        
        // TODO: NavigatorLanguage NavigatorLanguage { get; }//NavigatorLanguage.language
//NavigatorLanguage.languages
        // TODO: LockManager locks { get; }
        // TODO: MediaCapabilities mediaCapabilities { get; }
        int maxTouchPoints { get; }
        // TODO: MimeTypeArray mimeTypes { get; }
        bool onLine { get; }
        string oscpu { get; }
        // TODO: Permission permissions { get; }
        string platform { get; }
        // TODO: PluginArray plugins { get; }
        string product { get; }
        // TODO: ServiceWorkerContainer serviceWorker { get; }
        // TODO: StorageManager storage { get; }
        string userAgent { get; }
        bool webdriver { get; }
        // Non Standard
        // TODO: Timestamp buildID { get; }
        // TODO: CredentialsContainer credentials { get; }
        int deviceMemory { get; }
        string doNotTrack { get; }
        // TODO: MediaDevices mediaDevices { get; }
//Navigator.mozNotification
//Navigator.webkitNotification
//Navigator.mozSocial
//Navigator.presentation
//Navigator.productSub
//Navigator.securitypolicy
//Navigator.standalone
//Navigator.storageQuota
        string vendor { get; }
        string vendorSub { get; } 
//Navigator.webkitPointer
    }
}