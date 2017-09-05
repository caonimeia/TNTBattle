using System;
using UnityEngine;
using UnityEngine.Assertions;

public class MFLog {
    private static void Log(LogType logType, params object[] messages) {
        string message = "";
        foreach (object tmp in messages) {
            message = message + tmp + '\t';
        }

        message = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ") + message;
        Debug.unityLogger.Log(logType, message);
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    public static void LogInfo(params object[] messages) {
        Log(LogType.Log, messages);
    }

    /// <summary>
    /// 打印错误日志
    /// </summary>    
    public static void LogError(params object[] messages) {
        Log(LogType.Error, messages);
    }

    /// <summary>
    /// 打印警告日志
    /// </summary>
    public static void LogWarning(params object[] messages) {
        Log(LogType.Warning, messages);
    }

    public static void AssertNotNull(object obj) {
        Assert.IsNotNull(obj);
    }
}