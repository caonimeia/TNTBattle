using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MFFileReader {
    public static void Read(string path, Encoding encodeing) {
        try {
            if (!File.Exists(path)) {
                MFLog.LogError("File {0} Not Found" + path);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (StreamReader sr = new StreamReader(fs, encodeing)) {
                    int realLineCount = 0;
                    while (sr.Peek() > -1) {
                        string line = sr.ReadLine();
                        // 以 # 开头为注释
                        if (line.IndexOf("#") != 0) {
                            // 实际内容第一行 表头
                            if (realLineCount == 0) {

                            }
                            // tab文件以'\t'作为分隔符
                            string[] fields = line.Split('\t');
                            foreach (string field in fields) {
                                MFLog.LogInfo(field, field.Length);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e) {
            MFLog.LogError(e.ToString());
        }
    }

    public static void LoadTabFile(string path, string primaryKey) {
        Read(path, Encoding.Default);
    }
}
