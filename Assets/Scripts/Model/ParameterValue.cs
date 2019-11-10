using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Requirement", menuName = "DialogRequirement", order = 2)]
public class ParameterValue : ScriptableObject
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
    public float value;

    public bool Check(CharacterActionsController character)
    {
        switch (parameter)
        {
            case Parameter.HP:
                return CheckValue(character.hp.GetValue());
        }
        return false;
    }

    private bool CheckValue(float valueToCheck)
    {
        switch (comparison)
        {
            case Comparison.Equals:
                return valueToCheck.Equals(value);
            case Comparison.Greater:
                return valueToCheck < value;
            case Comparison.Less:
                return valueToCheck > value;
        }
        return false;
    }
}
