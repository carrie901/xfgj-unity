using UnityEngine;
using System.Collections;

public class IosAdjustor : MonoBehaviour {

    private static readonly string IOS_SYS_PREFIX = "iPhone OS";

    public enum OperatingSystem {
        ANDROID,
        IOS_5,
        IOS_6,
        IOS_7
    };

    void Start () {
        string operSys = SystemInfo.operatingSystem;
        OperatingSystem sys = JudgeSystem(operSys);
        if (sys == OperatingSystem.IOS_5) {
            Debug.Log("sys is ios 5");
        }
        else if (sys == OperatingSystem.IOS_6) {
            Debug.Log("sys is ios 6");
        }
        else if (sys == OperatingSystem.IOS_7) {
            Debug.Log("sys is ios 7");
            GameObject panel = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel");
            Vector3 position = panel.transform.localPosition;
            panel.transform.localPosition = new Vector3(position.x, position.y - 10, position.z);
        }
        else {
            Debug.Log("sys is android");
        }
    }

    private OperatingSystem JudgeSystem (string info) {
        if (info.StartsWith(IOS_SYS_PREFIX)) {
            char[] version = info.Substring(IOS_SYS_PREFIX.Length).ToCharArray();
            for (int i = 0; i < version.Length; ++i) {
                if (version[i] >= '0' && version[i] <= '9') {
                    switch(version[i]) {
                    case '5':
                        return OperatingSystem.IOS_5;
                    case '6':
                        return OperatingSystem.IOS_6;
                    case '7':
                        return OperatingSystem.IOS_7;
                    }
                }
            }
        }
        return OperatingSystem.ANDROID;
    }
    
}
