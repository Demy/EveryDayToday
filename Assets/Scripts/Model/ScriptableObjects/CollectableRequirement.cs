using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "Collectable", menuName = "Requirement/Collectable", order = 3)]
public class CollectableRequirement : Requirement
{
    public Flag flag;
    public int count;

    private int collected;

    public override bool Check(CharacterActionsController character)
    {
        return collected >= count;
    }

    public void Increment()
    {
        ++collected;
    }

    public override int GetValue()
    {
        return count;
    }
}
