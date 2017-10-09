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

    public void ReceiveBoom() {
        _boomObj.SetActive(true);
    }

    public void SendBoom(MFBoomComponent target) {
        target.ReceiveBoom();
        _boomObj.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
