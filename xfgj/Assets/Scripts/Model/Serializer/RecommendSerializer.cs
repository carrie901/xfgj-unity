using System;
using System.Collections.Generic;
using LitJson;

public class RecommendSerializer {

    private static string STATUS_DELETED = "deleted";

    public Recommend ToObject (string str) {
        if (string.IsNullOrEmpty(str)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Recommend((int)jd[Param.RECOMMEND_ID], (string)jd[Param.PICTURE_ID],
                             (string)jd[Param.URL], StringUtil.StringToDateTime((string)jd[Param.START_TIME]),
                             StringUtil.StringToDateTime((string)jd[Param.END_TIME]),
                             (string)jd[Param.POSITION]);
    }

    public List<Recommend> ToObjects (string str) {
        if (string.IsNullOrEmpty(str)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Recommend> list = new List<Recommend>();
        for (int i = 0; i < jd.Count; ++i) {
            Recommend recommend = ToObject(jd[i].ToJson());
            if (recommend != null) {
                list.Add(recommend);
            }
        }
        return list;
    }

    public static void ToObject (string str, out Recommend recommend, out bool isDeleted) {
        if (string.IsNullOrEmpty(str)) {
            recommend = null;
            isDeleted = false;
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        isDeleted = STATUS_DELETED.Equals((string)jd[Param.STATUS]);
        recommend = new Recommend((int)jd[Param.RECOMMEND_ID], (string)jd[Param.PICTURE_ID],
                             (string)jd[Param.URL], StringUtil.StringToDateTime((string)jd[Param.START_TIME]),
                             StringUtil.StringToDateTime((string)jd[Param.END_TIME]),
                             (string)jd[Param.POSITION]);
    }

    public static void ToObjects (string str, out List<Recommend> updateList,
                                  out List<Recommend> deleteList) {
        updateList = new List<Recommend>();
        deleteList = new List<Recommend>();
        if (string.IsNullOrEmpty(str)) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return;
        }
        for (int i = 0; i < jd.Count; ++i) {
            bool isDeleted = false;
            Recommend recommend;
            ToObject(jd[i].ToJson(), out recommend, out isDeleted);
            if (recommend == null) {
                continue;
            }
            if (isDeleted) {
                deleteList.Add(recommend);
            }
            else {
                updateList.Add(recommend);
            }
        }
    }

}

