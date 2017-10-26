using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAgent : MonoBehaviour {
    private float mTotalTime = 0;
    private MFNetManager _netMgr = MFNetManager.GetInstance();
    private MFInputMgr _inputMgr = MFInputMgr.GetInstance();

    private void Awake() {
        _netMgr.Init(new MFSocketClient("127.0.0.1", 2333, new MFJsonSerialzable()));
        _inputMgr.Init();

        MFUIMgr.Init();
        MFRobotMgr.Init();
    }

    // Use this for initialization
    private void Start() {
        MFNetManager.GetInstance().Connect();
        MFRobotMgr.AddSomeRobot();
    }

    // Update is called once per frame
    private void Update() {
        MFNetManager.GetInstance().Update();
        MFInputMgr.GetInstance().Update();
        //MFBoomMgr.Update();
        MFTimer.Update();
    }

    private void FixedUpdate() {
        mTotalTime += Time.deltaTime;
        if (mTotalTime > 1f) {
            //MFNetManager.GetInstance().Send("222");
            mTotalTime = 0f;
        }
    }

    private void OnDestroy() {
        MFNetManager.GetInstance().DisConnect();
    }
}
