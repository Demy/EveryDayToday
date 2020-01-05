using UnityEngine;

public class Requirement : ScriptableObject
{
    public virtual bool Check(CharacterActionsController character)
    {
        return false;
    }

    public virtual int GetValue()
    {
        return 0;
    }
}