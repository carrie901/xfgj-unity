using System;
using System.Collections.Generic;
using LitJson;

public class ItemSerializer
{

    public static Item ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new Item((long)jd[Param.NUM_IID], (string)jd[Param.TITLE], (string)jd[Param.DETAIL_URL],
                        (int)jd[Param.CID], (string)jd[Param.PIC_URL], (float)jd[Param.PRICE],
                        StringUtil.StringToDateTime((string)jd[Param.LIST_TIME]),
                        StringUtil.StringToDateTime((string)jd[Param.DELIST_TIME]),
                        (int)jd[Param.PRODUCT_ID], (int)jd[Param.SEQ],
                        StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<Item> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Item> list = new List<Item>();
        for (int i = 0; i < jd.Count; ++i) {
            Item item = ToObject(jd[i].ToJson());
            if (item != null) {
                list.Add(item);
            }
        }
        return list;
    }

}

