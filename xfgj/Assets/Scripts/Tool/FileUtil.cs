using System;
using System.IO;

public class FileUtil {

    public static bool IsFileExists (string file) {
        if (string.IsNullOrEmpty(file)) {
            return false;
        }
        return File.Exists(file);
    }

    public static byte[] ReadBinaryFile (string file) {
        if (!IsFileExists(file)) {
            return null;
        }
        FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        byte[] data = br.ReadBytes((int)fs.Length);
        br.Close();
        return data;
    }

}

