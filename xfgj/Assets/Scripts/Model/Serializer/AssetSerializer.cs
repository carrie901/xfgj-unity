using System;
using System.Collections.Generic;
using LitJson;

public class AssetSerializer {

    public static Asset ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Asset((int)jd[Param.ASSET_ID], (string)jd[Param.NAME],
                           (int)jd[Param.VERSION]);
    }

    public static List<Asset> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Asset> list = new List<Asset>();
        for (int i = 0; i < jd.Count; ++i) {
            Asset asset = ToObject(jd[i].ToJson());
            if (asset != null) {
                list.Add(asset);
            }
        }
        return list;
    }
}

