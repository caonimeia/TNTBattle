﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MFInputMgr {
    private static MFInputMgr _instance;
    private GameObject _character;
    private MFInputMgr() {

    }

    public static MFInputMgr GetInstance() {
        if (null == _instance) {
            _instance = new MFInputMgr();
        }

        return _instance;
    }

    public void Init() {
        
    }

    public void BindCharacter(GameObject character) {
        _character = character;
    }

    public void Update() {
        ProcessMoveInput();
    }

    private void ProcessMoveInput() {
        float moveX = 0;
        float moveZ = 0;

        if (Input.GetAxis("Horizontal") != 0)
            moveX = Input.GetAxis("Horizontal") * Time.deltaTime;

        if (Input.GetAxis("Vertical") != 0)
            moveZ = Input.GetAxis("Vertical") * Time.deltaTime;

        if (moveX != 0 || moveZ != 0)
            _character.GetComponent<MFMoveComponent>().Move(moveX, 0, moveZ);
    }
}
