using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;


public enum UILayer {
    main,
    fight,
}

public enum UIDisplayType {
    _2D,
    _3D,
}

public static class MFUIMgr {
    private class UIInfo {
        public GameObject prefab;
        public UILayer layer;
        // 差一个类型 单例 或 非单例
    }

    // 需要一个UI Object Pool
    // close 的时候不立即销毁


    private static Dictionary<Type, UIInfo> _uiPrefabDic;
    public static Camera camera2D;
    private static GameObject _mainUILayer;

    static MFUIMgr() {
        _uiPrefabDic = new Dictionary<Type, UIInfo>();
    }

    public static void Init() {
        camera2D = GameObject.Find("UIRoot/Camera").GetComponent<Camera>();
        Assert.IsNotNull(camera2D);

        _mainUILayer = GameObject.Find("UIRoot/MainUI");
        Assert.IsNotNull(_mainUILayer);

        MFUIList.Bind();
    }

    public static void Open<T>() {
        Type uiScript = typeof(T);
        UIInfo uiInfo;
        if(!_uiPrefabDic.TryGetValue(uiScript, out uiInfo)) {
            MFLog.LogError("UI脚本没有绑定Prefab");
            return;
        }

        GameObject uiObj = GameObject.Instantiate(uiInfo.prefab);
        Transform parent = _mainUILayer.transform; // default
        switch (uiInfo.layer) {
            case UILayer.main:
                parent = _mainUILayer.transform;
                break;
            case UILayer.fight:
                break;
        }
        uiObj.transform.SetParent(parent, false);
    }

    public static void BindPrefab(Type uiScript, string prefabPath, UILayer layer) {
        GameObject uiPrefab = MFResoureUtil.LoadPrefabFromPath(prefabPath);
        Assert.IsNotNull(uiPrefab);
        UIInfo info = new UIInfo {
            prefab = uiPrefab,
            layer = layer,
        };

        if (_uiPrefabDic.ContainsKey(uiScript)) {
            MFLog.LogError("UI脚本重复绑定!!!");
        }

        _uiPrefabDic.Add(uiScript, info);
    }
}
