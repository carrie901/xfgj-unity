using System;
using System.Collections.Generic;
using LitJson;

public class ProductSerializer
{
    private static string STATUS_NORMAL = "normal";
    private static string STATUS_DELETED = "deleted";

    public static Product ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
        return new Product((int)jd[Param.PRODUCT_ID], (int)jd[Param.PRODUCER_ID],
            (string)jd[Param.NAME], (int)jd[Param.CID], (string)jd[Param.SIZE],
            (string)jd[Param.PIC_URL], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]),
            (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
    }

    public static List<Product> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd = JsonMapper.ToObject(str);
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

    public static void ToObject (string str, out Product product, out bool isDeleted) {
        if (str == null || str.Equals(String.Empty)) {
            product = null;
            isDeleted = false;
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        isDeleted = STATUS_DELETED.Equals((string)jd[Param.STATUS]);
        product = new Product((int)jd[Param.PRODUCT_ID], (int)jd[Param.PRODUCER_ID],
            (string)jd[Param.NAME], (int)jd[Param.CID], (string)jd[Param.SIZE],
            (string)jd[Param.PIC_URL], (string)jd[Param.DETAILS],
            StringUtil.StringToDateTime((string)jd[Param.MODIFIED]),
            (string)jd[Param.ASSET_NAME], (int)jd[Param.ASSET_VERSION]);
    }

    public static void ToObjects (string str, out List<Product> updateList,
                                  out List<Product> deleteList) {
        updateList = new List<Product>();
        deleteList = new List<Product>();
        if (str == null || str.Equals(String.Empty)) {
            return;
        }
        JsonData jd = JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return;
        }
        for (int i = 0; i < jd.Count; ++i) {
            bool isDeleted = false;
            Product product;
            ToObject(jd[i].ToJson(), out product, out isDeleted);
            if (product == null) {
                continue;
            }
            if (isDeleted) {
                deleteList.Add(product);
            }
            else {
                updateList.Add(product);
            }
        }
    }
}

