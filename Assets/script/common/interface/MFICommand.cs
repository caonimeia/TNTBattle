

using System;
using UnityEngine;

public interface IMFMoveable {
    void Move(float x, float y, float z);
}

public interface IMFJumpable {
    void Jump();
}

public interface IMFCommand {
    void Execute(object obj, params object[] param);
}

public class MFMoveCommand {
    public void Execute(object obj, float x, float y, float z) {
        var o = (obj as IMFMoveable);
        if(o == null) {
            MFLog.LogError("");
            return;
        }
        o.Move(x, y, z);
    }
}

public class MFJumpCommand : IMFCommand {
    public void Execute(object obj, params object[] param) {
        var o = (obj as IMFJumpable);
        if (o == null) {
            MFLog.LogError("");
            return;
        }
        o.Jump();
    }
}