

public interface IMFCommand
{
    void Execute(object obj);
}

public class MFMoveCommand : IMFCommand
{
    public void Execute(object obj)
    {
        var mfMoveable = obj as IMFMoveable;

        if (mfMoveable != null) mfMoveable.Move();
    }
}


public interface IMFMoveable
{
    void Move();
}




public class Character : IMFMoveable
{
    public void Move()
    {

    }
}