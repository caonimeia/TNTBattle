using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MFBoomMgr {
    private static MFBoomComponent _sender;
    private static MFBoomComponent _receiver;

    public static void Record(MFBoomComponent sender, MFBoomComponent receiver) {
        _sender = sender;
        _receiver = receiver;
    }
}
