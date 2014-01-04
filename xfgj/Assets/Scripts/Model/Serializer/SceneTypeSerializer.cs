using System;
using System.Collections.Generic;
using LitJson;

public class SceneTypeSerializer
{
    private static string STATUS_DELETED = "deleted";

    public static SceneType ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new SceneType((int)jd[Param.TYPE_ID], (string)jd[Param.NAME], (string)jd[Param.PICTURE_ID],
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

    public static void ToObject (string str, out SceneType sceneType, out bool isDeleted) {
        if (str == null || str.Equals(String.Empty)) {
            sceneType = null;
            isDeleted = false;
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        isDeleted = STATUS_DELETED.Equals((string)jd[Param.STATUS]);
        sceneType = new SceneType((int)jd[Param.TYPE_ID], (string)jd[Param.NAME], (string)jd[Param.PICTURE_ID],
                                  StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static void ToObjects (string str, out List<SceneType> updateList,
                                  out List<SceneType> deleteList) {
        updateList = new List<SceneType>();
        deleteList = new List<SceneType>();
        if (str == null || str.Equals(String.Empty)) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return;
        }
        for (int i = 0; i < jd.Count; ++i) {
            bool isDeleted = false;
            SceneType sceneType;
            ToObject(jd[i].ToJson(), out sceneType, out isDeleted);
            if (sceneType == null) {
                continue;
            }
            if (isDeleted) {
                deleteList.Add(sceneType);
            }
            else {
                updateList.Add(sceneType);
            }
        }
    }

}

