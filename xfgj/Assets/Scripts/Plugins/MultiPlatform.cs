using System;

public class MultiPlatform
{
    public static string GetAppVersion () {
#if UNITY_IPHONE
        return IosPlugin.GetBundleVersion();
#elif UNITY_ANDROID
        return "";
#else
        return "";
#endif
    }

}

