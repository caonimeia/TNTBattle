using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MFMoveComponent))]
public class MFCharacter : MonoBehaviour {
    private void Awake() {
        MFInputMgr.GetInstance().BindCharacter(gameObject);
    }


}


