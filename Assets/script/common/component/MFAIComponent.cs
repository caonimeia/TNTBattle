using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo 改个名字 
public enum CharacterState {
    noBoom,
    hadBoom,
}

public class MFAIComponent : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private float _minRayDistance = 1f;
    public float obstacleRange = 5f;
    private CharacterState _curState;

    private void Awake() {
        _moveComp = GetComponent<MFMoveComponent>();
        _curState = CharacterState.noBoom;
    }

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update() {

        switch (_curState) {
            case CharacterState.noBoom:
                RandomMove();
                break;

            case CharacterState.hadBoom:
                CatchOther();
                break;
        }
        
    }

    private void RandomMove() {
        #region debug模式下显示射线
        if (MFApplicationUtil.IsOpenDebug()) {
            RaycastHit dhit;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out dhit))
                Debug.DrawLine(transform.position, dhit.point, Color.red);
        }
        #endregion

        RaycastHit hit;
        while (true) {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.distance < obstacleRange) {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                } else {
                    break;
                }
            }
        }

        _moveComp.Move(0, 0, Time.deltaTime);
    }

    private void CatchOther() {

    }
}
