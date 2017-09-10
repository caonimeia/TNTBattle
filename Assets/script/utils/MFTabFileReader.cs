using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


public class TestClass {
    public enum Te {
        a, b, c
    }

    public int id;
    public string name;
    public float rate;
    public Te te;
}

public class MFTabFileReader {
    private const string remarkSign         = "#";          // 以 # 开头为注释 
    private const string primaryKeySign     = "@";          // 以 @ 开头为主键
    private const char   splitterSign       = '\t';         // tab文件以'\t'作为分隔符

    private static List<T> Load<T>(string path, Encoding encodeing) {
        List<T> list = new List<T>();
        if (!File.Exists(path)) {
            MFLog.LogError("File {0} Not Found" + path);
            return null;
        }

        // 不确定在using范围内return会不会自动释放stream资源
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
            using (StreamReader sr = new StreamReader(fs, encodeing)) {

                #region 读取表头
                string[] fields = null;
                while (sr.Peek() > -1) {
                    string line = sr.ReadLine();
                    // 第一行非注释为表头
                    if (line.IndexOf(remarkSign) == 0) {
                        continue;
                    }

                    fields = line.Split(splitterSign);
                    break;
                }

                if (fields == null || fields.Length == 0 || !CheckTabFileField<T>(fields)) {
                    MFLog.LogError("File {0} Header Struct Error" + path);
                    return null;
                }
                #endregion

                // todo 添加主键    

                #region 读取表内容
                while (sr.Peek() > -1) {
                    string line = sr.ReadLine();
                    if (line.IndexOf(remarkSign) == 0) {
                        continue;
                    }

                    string[] datas = line.Split(splitterSign);
                    T inst = Activator.CreateInstance<T>();
                    Type t = inst.GetType();
                    for (int i = 0; i < fields.Length; i++) {
                        FieldInfo fi = t.GetField(fields[i]);
                        if (fi == null) {
                            continue;
                        }

                        fi.SetValue(inst, MFTypeUtil.ChangeType(datas[i], fi.FieldType));
                    }

                    list.Add(inst);
                }
                #endregion

            }
        }

        return list;
    }

    public static List<T> LoadTabFile<T>(string path, string primaryKey = null) {
        return Load<T>(path, Encoding.Default);
    }

    /// <summary>
    /// 检测tab配置表是否包含类的所有字段
    /// 类中的字段没有出现在配置表中则表示配置表有错误
    /// </summary>
    private static bool CheckTabFileField<T>(string[] fields) {
        HashSet<string> fieldSet = new HashSet<string>();
        foreach(string field in fields) {
            fieldSet.Add(field);
        }

        Type t = typeof(T);
        foreach (FieldInfo pi in t.GetFields()) {
            if (!fieldSet.Contains(pi.Name)) {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 获取tab配置表的主键 没有的话返回null
    /// </summary>
    private string GetPrimaryKey(string[] fields) {
        foreach(string field in fields) {
            if (field.IndexOf(primaryKeySign) != 0) {
                return field;
            }
        }

        return null;
    }
}
