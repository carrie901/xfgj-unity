using System;
using System.Collections.Generic;
using LitJson;

public class ProducerSerializer
{

    public static Producer ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new Producer((int)jd[Param.PRODUCER_ID], (string)jd[Param.NAME],
                            (string)jd[Param.DETAILS], StringUtil.StringToDateTime((string)jd[Param.MODIFIED]));
    }

    public static List<Producer> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Producer> list = new List<Producer>();
        for (int i = 0; i < jd.Count; ++i) {
            Producer producer = ToObject(jd[i].ToJson());
            if (producer != null) {
                list.Add(producer);
            }
        }
        return list;
    }


}

