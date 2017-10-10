using UnityEngine;


public class MFCharacter : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private MFBoomComponent _boomComp;

    private void Awake() {
        MFInputMgr.GetInstance().BindCharacter(this);
        _moveComp = gameObject.AddComponent<MFMoveComponent>();
        _boomComp = gameObject.AddComponent<MFBoomComponent>();

    
    }

    public void Move(float moveX, float moveY, float moveZ) {
        
        _moveComp.Move(moveX, moveY, moveZ);
    }

    public void StopMove() {
        
        _moveComp.StopMove();
    }

    public void ReceiveBoom() {
        _boomComp.ReceiveBoom();
    }
}


