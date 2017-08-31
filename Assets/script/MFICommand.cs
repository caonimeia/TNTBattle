

using System;

public interface IMFMoveable {
    void Move();
}

public interface IMFJumpable {
    void Jump();
}

public interface IMFCommand {
    void Execute(object obj);
}

public class MFMoveCommand : IMFCommand {
    public void Execute(object obj) {
        var o = (obj as IMFMoveable);
        if(o == null) {
            MFLog.LogError("");
            return;
        }
        o.Move();
    }
}

public class MFJumpCommand : IMFCommand {
    public void Execute(object obj) {
        var o = (obj as IMFJumpable);
        if (o == null) {
            MFLog.LogError("");
            return;
        }
        o.Jump();
    }
}


public class Character : IMFMoveable, IMFJumpable {
    public void Jump() {
        MFLog.LogInfo("character jump");
    }

    public void Move() {
        MFLog.LogInfo("character move");
    }
}