using System;
using System.Collections.Generic;
using LitJson;

public class CategorySerializer {
    public static Category ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Category((int)jd[Param.CID], (string)jd[Param.NAME],
                            (int)jd[Param.PARENT_CID], (bool)jd[Param.IS_PARENT],
                            (bool)jd[Param.USABLE], StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<Category> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Category> list = new List<Category>();
        for (int i = 0; i < jd.Count; ++i) {
            Category category = ToObject(jd[i].ToJson());
            if (category != null) {
                list.Add(category);
            }
        }
        return list;
    }

}

