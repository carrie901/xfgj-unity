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

    public static string GetFileNameWithoutExt (string fileName) {
        if (String.IsNullOrEmpty(fileName)) {
            return null;
        }
        fileName = GetFileName(fileName);
        int index = fileName.IndexOf('.');
        if (index == 0) {
            return "";
        }
        if (index == -1) {
            return fileName;
        }
        return fileName.Substring(0, index);
    }

    public static string GetFileName (string fileName) {
        if (String.IsNullOrEmpty(fileName)) {
            return null;
        }
        int index = fileName.LastIndexOf('/');
        if (index == -1) {
            return fileName;
        }
        if (index != fileName.Length - 1) {
            return fileName.Substring(index + 1);
        }
        return "";
    }

    public static string GetGuid () {
        return Guid.NewGuid().ToString("N");
    }
    
}
