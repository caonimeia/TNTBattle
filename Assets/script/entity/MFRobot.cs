using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFRobot : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private MFBoomComponent _boomComp;
    private MFAIComponent _aiComp;

    private void Awake() {
        _moveComp = gameObject.AddComponent<MFMoveComponent>();
        _boomComp = gameObject.AddComponent<MFBoomComponent>();
        _aiComp = gameObject.AddComponent<MFAIComponent>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
