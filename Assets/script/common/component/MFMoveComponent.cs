
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

public enum MoveState
{
    moving,
    stop,
}

public class MFMoveComponent : MonoBehaviour {
    public float speed = 10f;
    private Rigidbody _rigid;
    private MoveState _curMoveState;
    private Vector3 _curMovement;
    private Animation _animation;
    private GameObject _characterModel;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
        _curMoveState = MoveState.stop;

        _characterModel = MFGameObjectUtil.Find(this, "SKELETON");
        Assert.IsNotNull(_characterModel);
        _animation = _characterModel.GetComponent<Animation>();
        Assert.IsNotNull(_animation);
    }

    public void Move(float moveX, float moveY, float moveZ) {
        _animation.Play("Walk");
        _curMoveState = MoveState.moving;
        _curMovement = new Vector3(moveX, moveY, moveZ);

        #region debug模式下显示射线
        if (MFApplicationUtil.IsOpenDebug()) {
            RaycastHit dhit;
            if (Physics.Raycast(new Ray(gameObject.transform.position, _curMovement), out dhit))
                Debug.DrawLine(transform.position, dhit.point, Color.red);
        }
        #endregion        
    }

    private void StartMove() {
        _rigid.velocity = _curMovement * speed;
    }

    private void StartRotate() {
        float angle = Vector3.Angle(_characterModel.transform.forward, _curMovement);
        float direction = Vector3.Cross(_characterModel.transform.forward.normalized, _curMovement.normalized).y;
        if (direction > 0)
            _characterModel.transform.Rotate(0, angle, 0);
        else if (direction < 0)
            _characterModel.transform.Rotate(0, -angle, 0);
        else
            _characterModel.transform.Rotate(0, angle, 0);
    }

    public void StopMove() {
        _animation.Play("Idle1");
        _curMoveState = MoveState.stop;
        _curMovement = Vector3.zero;
    }

    public void SpeedUp(float addValue) {
        speed += addValue;
    }

    private void FixedUpdate() {
        
        switch (_curMoveState)
        {
            case MoveState.moving:
                StartMove();
                StartRotate();
                break;
            default:
                break;
        }
    }
}
