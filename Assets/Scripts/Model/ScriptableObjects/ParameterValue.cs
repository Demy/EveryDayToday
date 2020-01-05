using UnityEngine;

[CreateAssetMenu(fileName = "Parameter", menuName = "Requirement/Parameter", order = 2)]
public class ParameterValue : Requirement
{
    public enum Parameter
    {
        HP
    }
    public enum Comparison
    {
        Equals,
        Greater,
        Less
    }

    public Parameter parameter;
    public Comparison comparison;
    public int value;

    public override bool Check(CharacterActionsController character)
    {
        switch (parameter)
        {
            case Parameter.HP:
                return CheckValue(character.hp.GetValue());
        }
        return false;
    }

    private bool CheckValue(int valueToCheck)
    {
        switch (comparison)
        {
            case Comparison.Equals:
                return valueToCheck.Equals(value);
            case Comparison.Greater:
                return valueToCheck > value;
            case Comparison.Less:
                return valueToCheck < value;
        }
        return false;
    }

    public override int GetValue()
    {
        return value;
    }
}
