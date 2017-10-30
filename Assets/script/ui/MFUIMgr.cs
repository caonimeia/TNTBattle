using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;

public static class MFUIMgr {
    private const string prePath = "ui/";
    private static Dictionary<Type, GameObject> _uiPrefabDic;
    public static Camera camera2D;
    private static GameObject _mainUILayer;

    static MFUIMgr() {
        _uiPrefabDic = new Dictionary<Type, GameObject>();
    }

    public static void Init() {
        camera2D = GameObject.Find("UIRoot/Camera").GetComponent<Camera>();
        Assert.IsNotNull(camera2D);

        _mainUILayer = GameObject.Find("UIRoot/MainUI");
        Assert.IsNotNull(_mainUILayer);

        BindPrefab();
    }

    public static void Open<T>() {
        Type uiScript = typeof(T);
        GameObject uiPrefab;
        if(!_uiPrefabDic.TryGetValue(uiScript, out uiPrefab)) {
            MFLog.LogError("UI脚本没有绑定Prefab");
            return;
        }

        GameObject uiObj = GameObject.Instantiate(uiPrefab);
        uiObj.transform.SetParent(_mainUILayer.transform, false);
        //MFGameObjectUtil.Find()
    }

    // 需要个基类

    public static void BindPrefab() {
        _BindPrefab(typeof(MFBattleInfoView), prePath + "Battle/BattleInfoView.prefab");
    }

    // todo 重构加入层级
    private static void _BindPrefab(Type uiScript, string prefabPath) {
        GameObject uiPrefab = MFResoureUtil.LoadPrefabFromPath(prefabPath);
        Assert.IsNotNull(uiPrefab);
        if (_uiPrefabDic.ContainsKey(uiScript)) {
            MFLog.LogError("UI脚本重复绑定!!!");
        }

        _uiPrefabDic.Add(uiScript, uiPrefab);
    }
}
