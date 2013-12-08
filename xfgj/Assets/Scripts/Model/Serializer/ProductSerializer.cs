using System;
using System.Collections.Generic;
using LitJson;

public class ProductSerializer
{

    public static Product ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new Product((int)jd[Param.PRODUCT_ID], (int)jd[Param.PRODUCER_ID],
            (string)jd[Param.NAME], (int)jd[Param.CID], (string)jd[Param.SIZE],
            (string)jd[Param.PIC_URL], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<Product> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Product> list = new List<Product>();
        for (int i = 0; i < jd.Count; ++i) {
            Product product = ToObject(jd[i].ToJson());
            if (product != null) {
                list.Add(product);
            }
        }
        return list;
    }

}

