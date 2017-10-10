using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MFInputMgr {
    private static MFInputMgr _instance;
    private MFCharacter _character;
    private MFInputMgr() {

    }

    public static MFInputMgr GetInstance() {
        if (null == _instance) {
            _instance = new MFInputMgr();
        }

        return _instance;
    }

    public void Init() {
        
    }

    public void BindCharacter(MFCharacter character) {
        _character = character;
    }

    public void Update() {
        ProcessCommand();
        ProcessMoveInput();
    }

    private void ProcessMoveInput() {
        float moveX = 0;
        float moveZ = 0;

        if (Input.GetAxis("Horizontal") != 0)
            moveX = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") != 0)
            moveZ = Input.GetAxis("Vertical");

        Vector3 toward = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            toward.x = -1f;
        if (Input.GetKey(KeyCode.D))
            toward.x = 1f;
        if (Input.GetKey(KeyCode.W))
            toward.z = 1f;
        if (Input.GetKey(KeyCode.S))
            toward.z = -1f;

        if (moveX != 0 || moveZ != 0)
            _character.Move(toward.x, 0, toward.z);
        else
            _character.StopMove();
    }

    private void ProcessCommand() {
        // 切换debug模式
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
            MFApplicationUtil.SwitchDebugMode();

        // 把炸弹装在自己头上
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
            _character.ReceiveBoom();

    }
}
