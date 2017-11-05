using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrefabPath {
    private const string prePath = "ui/";
    public const string battleInfoView = prePath + "Battle/BattleInfoView.prefab";
}

public static class MFUIList {
    public static void Bind() {
        MFUIMgr.BindPrefab<MFBattleInfoView>(UIPrefabPath.battleInfoView, UILayer.main, UIInstanceType.single);
    }
}
