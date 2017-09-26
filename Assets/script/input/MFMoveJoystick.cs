using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MFMoveJoystick : MonoBehaviour {
    private MFCharacter _character; // todo 换种绑定角色的方案
    private Button stickButton;
    private Image stickBG;

    private void Awake() {
        Init();
        RegisterEvent();
    }

    private void Start() {
        stickBG.gameObject.SetActive(false);
    }

    private void Init() {
        _character = GameObject.Find("character").GetComponent<MFCharacter>();
        stickButton = MFGameObjectUtil.Find<Button>(gameObject, "Button");
        stickBG = MFGameObjectUtil.Find<Image>(gameObject, "BGImg");

        Assert.IsNotNull(_character);
        Assert.IsNotNull(stickButton);
        Assert.IsNotNull(stickBG);
    }

    private void RegisterEvent() {
        stickButton.onClick.AddListener(OnStickButtonClick);
        
    }


    private void OnStickButtonClick() {
        Vector3 result = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(gameObject.GetComponent<RectTransform>(), Vector2.zero, MFUIMgr.camera2D, out result);
        MFLog.LogInfo(result);
        //MFLog.LogInfo(Input.mousePosition);
        //_character.Move(1, 0, 1);
    }


}
