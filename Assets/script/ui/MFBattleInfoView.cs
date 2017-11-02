using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MFBattleInfoView : MFUIBase {
    private Text leftTime;
    private int timerId;
    private int sec = 60;

    // 秒数转成 时间格式 HH:MM:SS

    // Awake 一般用于初始化组件
    protected override void Awake() {
        base.Awake();

        leftTime = MFGameObjectUtil.Find<Text>(gameObject, "LeftTime/Text");
        Assert.IsNotNull(leftTime);
    }

    // Start 一般的业务初始化
    protected override void Start() {
        base.Start();

        timerId = MFTimer.RegisterSchedule(1f, () => {
            leftTime.text = (--sec).ToString();
        });
    }
}
