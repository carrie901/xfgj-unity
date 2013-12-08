using System;
using System.Collections.Generic;
using LitJson;

public class TraderateSerializer
{

    public static Traderate ToObject (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        return new Traderate((int)jd[Param.TID], (long)jd[Param.NUM_IID],
            Traderate.StringToROLE((string)jd[Param.ROLE]), (string)jd[Param.NICK],
            Traderate.StringToRateResult((string)jd[Param.RESULT]),
            StringUtil.StringToDateTime((string)jd[Param.CREATED]),
            (string)jd[Param.CONTENT], (string)jd[Param.REPLY]);
    }

    public static List<Traderate> ToObjects (string str) {
        if (str == null || str.Equals(String.Empty)) {
            return null;
        }
        JsonData jd =JsonMapper.ToObject(str);
        if (!jd.IsArray) {
            return null;
        }
        List<Traderate> list = new List<Traderate>();
        for (int i = 0; i < jd.Count; ++i) {
            Traderate traderate = ToObject(jd[i].ToJson());
            if (traderate != null) {
                list.Add(traderate);
            }
        }
        return list;
    }

}

