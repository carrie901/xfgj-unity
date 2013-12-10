using System;
using System.Globalization;
using System.Collections;

public class StringUtil {

    
    public static string DateTimeToString (DateTime dt) {
        string format = "{0:" + Config.DATETIME_FORMAT + "}";
        return string.Format(format, dt);
    }
    
    public static DateTime StringToDateTime (string s) {
        DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        dtFormat.LongTimePattern = Config.DATETIME_FORMAT;
        return Convert.ToDateTime(s, dtFormat);
    }

    public static string FileNameWithoutExt (string fileName) {
        if (String.IsNullOrEmpty(fileName)) {
            return null;
        }
        int index = fileName.IndexOf('.');
        if (index == 0) {
            return "";
        }
        if (index == -1) {
            return fileName;
        }
        return fileName.Substring(0, index);
    }
    
}
