using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetUtil {

    public static IEnumerator Post (string url, Dictionary<string, string> post,
                                    ResponseHandle callback) {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<string, string> postArg in post) {
            form.AddField(postArg.Key, postArg.Value);
        }
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null) {
            Debug.Log("error is :"+ www.error);
            if (callback != null) {
                callback(null);
            }
        }
        else {
            if (callback != null) {
                callback(www.text);
            }
        }
    }

    public static IEnumerator Upload (string url, Dictionary<string, string> post,
                                      Dictionary<string, string> files,
                                      ResponseHandle callback) {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<string, string> postArg in post) {
            form.AddField(postArg.Key, postArg.Value);
        }
        foreach(KeyValuePair<string, string> fileArg in files) {
            if (FileUtil.IsFileExists(fileArg.Value)) {
                byte[] data = FileUtil.ReadBinaryFile(fileArg.Value);
                form.AddBinaryData(fileArg.Key, data, StringUtil.GetFileName(fileArg.Value));
            }
        }
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null) {
            Debug.Log("error is :"+ www.error);
            if (callback != null) {
                callback(null);
            }
        }
        else {
            if (callback != null) {
                callback(www.text);
            }
        }
    }

    public static IEnumerator Get (string url, ResponseHandle callback) {
        WWW www = new WWW (url);
        yield return www;
        if (www.error != null) {
            Debug.Log("error is :"+ www.error);
            if (callback != null) {
                callback(null);
            }
        }
        else {
            if (callback != null) {
                callback(www.text);
            }
        }
    }

}
