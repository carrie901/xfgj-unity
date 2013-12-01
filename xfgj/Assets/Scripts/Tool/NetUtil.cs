using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetUtil {

    public static IEnumerator Post (string url, Dictionary<string,string> post,
                                    ApiCaller.ResponseHandle callback) {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<string,string> post_arg in post) {
            form.AddField(post_arg.Key, post_arg.Value);
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

    public static IEnumerator Get (string url, ApiCaller.ResponseHandle callback) {
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
