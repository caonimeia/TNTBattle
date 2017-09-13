using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFAIComponent : MonoBehaviour {
    private MFMoveComponent _moveComp;

    private void Awake() {
        _moveComp = GetComponent<MFMoveComponent>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _moveComp.Move(0.1f * Time.deltaTime, 0, 0.1f * Time.deltaTime);
    }
}
