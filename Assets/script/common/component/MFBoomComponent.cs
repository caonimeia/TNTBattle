using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BoomState {
    idle,
    withBoom,
    onSendBoom,
}

public class MFBoomComponent : MonoBehaviour {
    private GameObject _boomObj;
    private bool _withBoom;
    private MFBoomComponent _target;
    private float _leftTime;
    private BoomState _boomState;
    private float _passTime;
    private int _timerId;

    private void Awake() {
        _boomObj = MFGameObjectUtil.Find(this, "boom");
        Assert.IsNotNull(_boomObj);
        _withBoom = false;
        _leftTime = 0;
        _boomState = BoomState.idle;
        _passTime = 0;
        _timerId = 0;
    }

    public void ReceiveBoom(float leftTime) {
        _boomObj.SetActive(true);
        _withBoom = true;
        _leftTime = leftTime;
        _boomState = BoomState.withBoom;

        MFLog.LogInfo(_leftTime);
        _timerId = MFTimer.RegisterOnce(_leftTime, () => {
            Boom();
        });

        MFUIMgr.Open<MFBattleInfoView>();
    }

    public void SendBoom(MFBoomComponent target, float leftTime) {
        target.ReceiveBoom(leftTime);
        _boomObj.SetActive(false);
        _withBoom = false;
        _leftTime = 0;

        MFTimer.UnRegister(_timerId);
    }

    private void OnCollisionEnter(Collision collision) {
        MFBoomComponent cmp = collision.gameObject.GetComponent<MFBoomComponent>();
        if (cmp && _withBoom) {
            _target = cmp;
            _boomState = BoomState.onSendBoom;
        }
    }

    private void Boom() {
        MFUIMgr.Close<MFBattleInfoView>();
        MFLog.LogInfo("Boom!!!!!");
    }

    private void Update() {
        switch (_boomState) {
            case BoomState.withBoom:
                _passTime += Time.deltaTime;
                if (_passTime > 1.0f) {
                    _leftTime--;
                    _passTime = 0;
                }
                break;
            case BoomState.onSendBoom:
                if (_target && _withBoom) {
                    SendBoom(_target, _leftTime);
                    _target = null;
                }
                break;
            default:
                break;
        }
    }
}
