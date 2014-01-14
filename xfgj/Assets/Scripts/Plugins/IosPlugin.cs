using System;
using System.Runtime.InteropServices;

public class IosPlugin {

    [DllImport ("__Internal")]
    public static extern string GetBundleVersion ();

    [DllImport ("__Internal")]
    public static extern void OpenWebsite(string url);

    [DllImport ("__Internal")]
    public static extern long GetAvailableSpace();
}

