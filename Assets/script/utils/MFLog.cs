using System;
using UnityEngine;
using UnityEngine.Assertions;

public class MFLog {
    private static void Log(LogType logType, string message) {
        message = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ") + message;
        Debug.logger.Log(logType, message);
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    public static void LogInfo(object message) {
        Log(LogType.Log, message as string);
    }

    /// <summary>
    /// 打印错误日志
    /// </summary>    
    public static void LogError(string message) {
        Log(LogType.Error, message);
    }

    /// <summary>
    /// 打印警告日志
    /// </summary>
    public static void LogWarning(string message) {
        Log(LogType.Warning, message);
    }

    public static void AssertNotNull(object obj) {
        Assert.IsNotNull(obj);
    }
}