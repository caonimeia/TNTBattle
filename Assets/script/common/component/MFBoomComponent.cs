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
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 0.8f, 1 << LayerMask.NameToLayer(LayerMaskDef.player))) {
            SendBoom(hit.transform.gameObject.GetComponent<MFBoomComponent>());
        }
    }

    public void ReceiveBoom() {
        _boomObj.SetActive(true);
    }

    public void SendBoom(MFBoomComponent target) {
        target.ReceiveBoom();
        _boomObj.SetActive(false);
    }
}
