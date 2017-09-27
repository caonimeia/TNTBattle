using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFAIComponent : MonoBehaviour {
    private MFMoveComponent _moveComp;
    public float obstacleRange = 5f;

    private void Awake() {
        _moveComp = GetComponent<MFMoveComponent>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 10 * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit)) {
           
            if (hit.distance < obstacleRange) {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }

        
        //_moveComp.Move(0.1f * Time.deltaTime, 0, 0.1f * Time.deltaTime);
    }
}
