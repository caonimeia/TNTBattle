using UnityEngine;


public class MFCharacter : MonoBehaviour {
    private MFMoveComponent _moveComp;
    private MFBoomComponent _boomComp;
    private Camera _mainCamera;
    private Vector3 _offset;

    public float distanceUp = 15f;
    public float distanceAway = 10f;
    public float smooth = 2f;//位置平滑移动值

    private void Awake() {
        MFInputMgr.GetInstance().BindCharacter(this);
        _moveComp = gameObject.AddComponent<MFMoveComponent>();
        _boomComp = gameObject.AddComponent<MFBoomComponent>();
        _mainCamera = Camera.main;
        _offset = gameObject.transform.position - _mainCamera.transform.position;
    }

    public void Move(float moveX, float moveY, float moveZ) {
        _moveComp.Move(moveX, moveY, moveZ);
    }

    public void StopMove() {
        _moveComp.StopMove();
    }

    public void ReceiveBoom(float leftTime) {
        _boomComp.ReceiveBoom(leftTime);
    }
}


