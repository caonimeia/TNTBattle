
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
    private float angleY = 0f;

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

        //transform.RotateAround(transform.position, Vector3.up, Vector3.Angle(transform.forward, _curMovement));
        
        #region debug模式下显示射线
        if (MFApplicationUtil.IsOpenDebug()) {
            RaycastHit dhit;
            if (Physics.Raycast(new Ray(transform.position, _curMovement), out dhit))
                Debug.DrawLine(transform.position, dhit.point, Color.red);
        }
        #endregion        
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
                _rigid.velocity = _curMovement * speed;
                float angle = Vector3.Angle(_characterModel.transform.forward, _curMovement);
                //_characterModel.transform.RotateAround(transform.position, Vector3.up, angle);
                _characterModel.transform.DOLocalRotate(new Vector3(0, angle, 0), 0.5f);
                break;
            default:
                break;
        }
    }
}
