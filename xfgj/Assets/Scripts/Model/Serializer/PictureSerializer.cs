using System;
using System.Collections.Generic;
using LitJson;

public class PictureSerializer {

    public static Picture ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Picture((string)jd[Param.PICTURE_ID], (string)jd[Param.ATLAS_NAME],
                           (int)jd[Param.ASSET_ID]);
    }

    public static List<Picture> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Picture> list = new List<Picture>();
        for (int i = 0; i < jd.Count; ++i) {
            Picture picture = ToObject(jd[i].ToJson());
            if (picture != null) {
                list.Add(picture);
            }
        }
        return list;
    }
}

