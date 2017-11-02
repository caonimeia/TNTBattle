using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MFUIList {
    private const string prePath = "ui/";

    public static void Bind() {
        MFUIMgr.BindPrefab(typeof(MFBattleInfoView), prePath + "Battle/BattleInfoView.prefab", UILayer.main);
    }
}
