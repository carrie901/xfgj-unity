using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;

public class SceneSerializer
{
    private static string STATUS_NORMAL = "normal";
    private static string STATUS_DELETED = "deleted";


    public static Scene ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Scene((int)jd[Param.SCENE_ID], (string)jd[Param.NAME],
            (int)jd[Param.TYPE_ID], (string)jd[Param.PICTURE_ID], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]),
            (int)jd[Param.ASSET_ID], (string)jd[Param.PRODUCTS]);
    }

    public static List<Scene> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Scene> list = new List<Scene>();
        for (int i = 0; i < jd.Count; ++i) {
            Scene scene = ToObject(jd[i].ToJson());
            if (scene != null) {
                list.Add(scene);
            }
        }
        return list;
    }

    public static void ToObject (string str, out Scene scene, out bool isDeleted) {
        if (str == null || str.Equals(String.Empty)) {
            scene = null;
            isDeleted = false;
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        isDeleted = STATUS_DELETED.Equals((string)jd[Param.STATUS]);
        scene = new Scene((int)jd[Param.SCENE_ID], (string)jd[Param.NAME],
            (int)jd[Param.TYPE_ID], (string)jd[Param.PICTURE_ID], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]),
            (int)jd[Param.ASSET_ID], (string)jd[Param.PRODUCTS]);
    }

    public static void ToObjects (string str, out List<Scene> updateList,
                                  out List<Scene> deleteList) {
        updateList = new List<Scene>();
        deleteList = new List<Scene>();
        if (str == null || str.Equals(String.Empty)) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return;
        }
        for (int i = 0; i < jd.Count; ++i) {
            bool isDeleted = false;
            Scene scene;
            ToObject(jd[i].ToJson(), out scene, out isDeleted);
            if (scene == null) {
                continue;
            }
            if (isDeleted) {
                deleteList.Add(scene);
            }
            else {
                updateList.Add(scene);
            }
        }
    }

}

