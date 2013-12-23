using System;
using System.Runtime.InteropServices;

public class IosPlugin {

    [DllImport ("__Internal")]
    public static extern string GetBundleVersion ();
}

