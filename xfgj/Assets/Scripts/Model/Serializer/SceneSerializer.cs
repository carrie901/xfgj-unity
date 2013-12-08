using System;
using System.Collections.Generic;
using LitJson;

public class SceneSerializer
{

    public static Scene ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new Scene((int)jd[Param.SCENE_ID], (string)jd[Param.NAME],
            (int)jd[Param.TYPE_ID], (string)jd[Param.PIC_URL], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<Scene> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
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

}

