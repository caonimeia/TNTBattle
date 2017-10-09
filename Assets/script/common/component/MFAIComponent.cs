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
    public float obstacleRange = 5f;
    private CharacterState _curState;

    private float _curMoveX;
    private float _curMoveZ;

    private void Awake() {
        _moveComp = GetComponent<MFMoveComponent>();
        _curState = CharacterState.noBoom;
        _curMoveX = Random.Range(-1, 1);
        _curMoveZ = Random.Range(-1, 1);
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
                //CatchOther();
                break;
        }
        
    }

    private void RandomMove() {
        RaycastHit hit;
        while (true) {
            Ray ray = new Ray(transform.position, new Vector3(_curMoveX, 0, _curMoveZ));
            if (Physics.Raycast(ray, out hit, obstacleRange, 1 << LayerMask.NameToLayer(MFLayerMaskDef.wall))) {
                _curMoveX = Random.Range(-1, 1);
                _curMoveZ = Random.Range(-1, 1);
                continue;
            }

            break;
        }

        _moveComp.Move(_curMoveX * Time.deltaTime, 0, _curMoveZ * Time.deltaTime);
    }
}
