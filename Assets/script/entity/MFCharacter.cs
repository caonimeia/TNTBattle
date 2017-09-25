using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MFMoveComponent))]
public class MFCharacter : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private void Awake() {
        MFInputMgr.GetInstance().BindCharacter(this);
        _moveComp = GetComponent<MFMoveComponent>();
    }

    public void Move(float moveX, float moveY, float moveZ) {
        _moveComp.Move(moveX, moveY, moveZ);
    }
}


