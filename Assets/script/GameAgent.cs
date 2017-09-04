using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAgent : MonoBehaviour {
    private float mTotalTime = 0;
    private void Awake() {
        MFNetManager.GetInstance().Init(new MFSocketClient("127.0.0.1", 2333, new MFJsonSerialzable()));
        Character c = new Character();
        IMFCommand command = new MFMoveCommand();
        command.Execute(c);
        command = new MFJumpCommand();
        command.Execute(c);
    }
    // Use this for initialization
    private void Start() {
        MFNetManager.GetInstance().Connect();
    }

    // Update is called once per frame
    private void Update() {
        MFNetManager.GetInstance().Update();
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
