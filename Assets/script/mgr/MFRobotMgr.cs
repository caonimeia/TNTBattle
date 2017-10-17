using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class MFRobotMgr {
    private static List<GameObject> robotList;
    private static GameObject robotPrefab;
    private static readonly string robotPrefabName = "robot.prefab";

    static MFRobotMgr() {
        robotList = new List<GameObject>();
    }

    public static void Init() {
        robotPrefab = MFResoureUtil.LoadPrefabFromPath(robotPrefabName);
        Assert.IsNotNull(robotPrefab);
    }

    public static void AddRobot(Vector3 position) {
        GameObject newRobot = Object.Instantiate(robotPrefab);
        if (position == null)
            position = Vector3.zero;

        newRobot.transform.position = position;
        robotList.Add(newRobot);
    }


    public static void AddSomeRobot() {
        Vector3 postion = Vector3.zero;
        for (int i = 0; i < 5; i++) {
            AddRobot(postion);
            postion.x += 10;
        }
    }

}
