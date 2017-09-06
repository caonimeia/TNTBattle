using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


public class TabFile {

}

public class TestClass {
    public int id;
    public string name;
    public float rate;
}

public class MFFileReader {
    public static void Read<T>(string path, Encoding encodeing) where T : new() {
        try {
            if (!File.Exists(path)) {
                MFLog.LogError("File {0} Not Found" + path);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (StreamReader sr = new StreamReader(fs, encodeing)) {

                    #region 读取表头
                    string[] fields = null;
                    while (sr.Peek() > -1) {
                        string line = sr.ReadLine();
                        // 以 # 开头为注释 
                        // 第一行非注释为表头
                        if (line.IndexOf("#") == 0) {
                            continue;
                        }

                        // tab文件以'\t'作为分隔符
                        fields = line.Split('\t');
                        break;
                    }

                    if (fields == null || fields.Length == 0) {
                        MFLog.LogError("File {0} Header Struct Error" + path);
                        return;
                    }
                    #endregion

                    // todo 需要检测class的属性在tab文件中是否存在

                    //MFLog.LogInfo(fields);
                    //if (!SameStructWithClass<T>(fields)) {
                    //    MFLog.LogError(string.Format("File [{0}] Header Struct Not Same With Class: {1}", 
                    //        path, typeof(T).ToString()));
                    //    return;
                    //}

                    Dictionary<int, T> dic = new Dictionary<int, T>();
                    int index = 1;
                    while (sr.Peek() > -1) {
                        string line = sr.ReadLine();
                        if (line.IndexOf("#") != 0) {
                            string[] datas = line.Split('\t');
                            T inst = new T();
                            Type t = inst.GetType();
                            for (int i = 0; i < fields.Length; i++) {
                                string d = datas[i];
                                if (d == "") {
                                    // todo 加上空值处理
                                } else {
                                    FieldInfo fi = t.GetField(fields[i]);
                                    if (fi != null) {
                                        // todo Convert.ChangeType不能处理枚举 还需要加上对枚举变量的处理
                                        var value = Convert.ChangeType(d, fi.FieldType);
                                        fi.SetValue(inst, value);
                                    }
                                }
                            }
                            dic.Add(index, inst);
                            index++;
                        }
                    }

                    foreach(var item in dic) {
                        TestClass tc = item.Value as TestClass;
                        MFLog.LogInfo(tc.id, tc.name, tc.rate);
                        MFLog.LogInfo(tc.id.GetType().ToString(), tc.name.GetType().ToString(), tc.rate.GetType().ToString());
                    }
                }
            }
        }
        catch (Exception e) {
            MFLog.LogError(e.ToString());
        }
    }

    //private static bool SameStructWithClass<T>(string[] fields) {
    //    Type t = typeof(T);
    //    foreach (string field in fields) {
    //        if (t.GetField(field) == null) {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    private static string[] ReadHeader(StreamReader sr) { 
        if (sr.Peek() == -1) {
            return null;
        }

        string line = sr.ReadLine();
        string[] fields = line.Split('\t');
        return fields;
    }

    public static void LoadTabFile<T>(string path, string primaryKey) where T : new() {
        Read<T>(path, Encoding.Default);
    }

    static public object ChangeType(object value, Type type) {
        if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
        if (value == null) return null;
        if (type == value.GetType()) return value;
        if (type.IsEnum) {
            if (value is string)
                return Enum.Parse(type, value as string);
            else
                return Enum.ToObject(type, value);
        }
        if (!type.IsInterface && type.IsGenericType) {
            Type innerType = type.GetGenericArguments()[0];
            object innerValue = ChangeType(value, innerType);
            return Activator.CreateInstance(type, new object[] { innerValue });
        }
        if (value is string && type == typeof(Guid)) return new Guid(value as string);
        if (value is string && type == typeof(Version)) return new Version(value as string);
        if (!(value is IConvertible)) return value;
        return Convert.ChangeType(value, type);
    }
}
