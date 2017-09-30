using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(MFBoomComponent))]
[RequireComponent(typeof(MFMoveComponent))]
public class MFCharacter : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private MFBoomComponent _boomComp;

    private void Awake() {
        MFInputMgr.GetInstance().BindCharacter(this);
        _moveComp = GetComponent<MFMoveComponent>();
        _boomComp = GetComponent<MFBoomComponent>();
    }

    public void Move(float moveX, float moveY, float moveZ) {
        _moveComp.Move(moveX, moveY, moveZ);
    }

    public void ReceiveBoom() {
        _boomComp.ReceiveBoom();
    }

    public void OnCollide(GameObject target) {

    }
}


