using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest", order = 1)]
public class QuestStructure : ScriptableObject
{
    public string questId;
    public Requirement[] successRequirements;
    public Requirement[] failRequirements;
    public string title;
    public string description;

    public StateChange[] rewards;

    public bool Check(CharacterActionsController character)
    {
        foreach (Requirement req in successRequirements)
        {
            if (!req.Check(character))
                return false;
        }
        foreach (Requirement req in failRequirements)
        {
            if (req.Check(character))
                return false;
        }
        return true;
    }
}
