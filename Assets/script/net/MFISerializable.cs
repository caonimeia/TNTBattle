using UnityEngine;
using System;
using System.Text;

public interface MFISerializable {
    byte[] Serialize(object o);
    T DeSerialize<T>(byte[] b);
}

public class MFJsonSerialzable : MFISerializable {
    /// <summary>
    /// 将对象转化为Json格式的字节数组
    /// </summary>
    public byte[] Serialize(object o) {
        string json = JsonUtility.ToJson(o);
        byte[] b = Encoding.UTF8.GetBytes(json);
        return b;
    }

    /// <summary>
    /// 将Json格式的字节数组转化为对象
    /// </summary>
    public T DeSerialize<T>(byte[] b) {
        string json = Encoding.UTF8.GetString(b);
        T obj = JsonUtility.FromJson<T>(json);
        return obj;
    }
}

[Serializable]
public class TestA {
    public int a;
    public int[] b;
}