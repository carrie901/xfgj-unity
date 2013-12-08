using System;
using System.Collections.Generic;
using LitJson;

public class SceneTypeSerializer
{

    public static SceneType ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new SceneType((int)jd[Param.TYPE_ID], (string)jd[Param.NAME],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<SceneType> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<SceneType> list = new List<SceneType>();
        for (int i = 0; i < jd.Count; ++i) {
            SceneType sceneType = ToObject(jd[i].ToJson());
            if (sceneType != null) {
                list.Add(sceneType);
            }
        }
        return list;
    }

}

