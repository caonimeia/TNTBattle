using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MFBoomComponent : MonoBehaviour {
    private GameObject _boomObj;
   
    private void Awake() {
        _boomObj = MFGameObjectUtil.Find(this, "boom");
        Assert.IsNotNull(_boomObj);
    }

    private void Update() {

    }

    public void ReceiveBoom() {
        _boomObj.SetActive(true);
    }
}
