using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MFTimer {
    private static int _curTimerId = 0;
    private static Dictionary<float, List<CallBackStruct>> eventsDic;
    private struct TimerStruct {
        public float time;
        public int index;
        public TimerStruct(float time, int index) {
            this.time = time;
            this.index = index;
        }
    }

    private class CallBackStruct {
        public Action action;
        public bool active;
        public CallBackStruct(Action action, bool active) {
            this.action = action;
            this.active = active;
        }
    }


    private static Dictionary<int, TimerStruct> timerStructDic;


    static MFTimer() {
        eventsDic = new Dictionary<float, List<CallBackStruct>>();
        timerStructDic = new Dictionary<int, TimerStruct>();
    }

    /// <summary>
    /// 注册定时器 精确到0.1秒
    /// </summary>
    public static int Register(float delayTime, Action callBack) {
        delayTime = MFMath.OneDecimal(delayTime);
        float curTime = MFMath.OneDecimal(Time.time);
        float callBackTime = curTime + delayTime;

        if (!eventsDic.ContainsKey(callBackTime))
            eventsDic[callBackTime] = new List<CallBackStruct>();

        eventsDic[callBackTime].Add(new CallBackStruct(callBack, true));
        timerStructDic.Add(_curTimerId++, new TimerStruct(callBackTime, eventsDic[callBackTime].Count - 1));
        return _curTimerId;
    }

    /// <summary>
    /// 注销定时器
    /// </summary>
    public static void UnRegister(int timerId) {
        if (!timerStructDic.ContainsKey(timerId)) {
            return;
        }

        TimerStruct ts = timerStructDic[timerId];
        List<CallBackStruct> cbsList;
        if (!eventsDic.TryGetValue(ts.time, out cbsList)) {
            return;
        }

        cbsList[ts.index].active = false;
    }

    public static void Update() {
        float curTime = MFMath.OneDecimal(Time.time);
        List<CallBackStruct> cbsList;
        if (!eventsDic.TryGetValue(curTime, out cbsList)) {
            return;
        }

        for (int i = 0; i < cbsList.Count; i++) {
            if (cbsList[i].active)
                cbsList[i].action();
        }

        cbsList.Clear();
        eventsDic.Remove(curTime);
    }
}