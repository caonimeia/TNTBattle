using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MFInputMgr {
    private static MFInputMgr _instance;
    private Character _character;
    private MFInputMgr() {

    }
    public static MFInputMgr GetInstance() {
        if (null == _instance) {
            _instance = new MFInputMgr();
        }

        return _instance;
    }

    public void Init(Character c) {
        BindCharacter(c);
    }

    public void BindCharacter(Character c) {
        _character = c;
    }

    public void Update() {
        //float translation = Input.GetAxis("Vertical") * speed;
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        //transform.Translate(0, 0, translation);
        //transform.Rotate(0, rotation, 0);

        float moveX = 0;
        float moveZ = 0;
        if (Input.GetAxis("Horizontal") != 0) {
            moveX = Input.GetAxis("Horizontal") * 1.0f * Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") != 0) {
            moveZ = Input.GetAxis("Vertical") * 1.0f * Time.deltaTime;
        }

        //Vector3 move = new Vector3(moveX, 0, moveZ);
        MFMoveCommand cmd = new MFMoveCommand();
        cmd.Execute(_character, moveX, 0, moveZ);
    }
}
