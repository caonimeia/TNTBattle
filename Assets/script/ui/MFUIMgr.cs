using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public static class MFUIMgr {
    public static Camera camera2D;
    public static void Init() {
        camera2D = GameObject.Find("UIRoot/Camera").GetComponent<Camera>();
        Assert.IsNotNull(camera2D);
    }


}
