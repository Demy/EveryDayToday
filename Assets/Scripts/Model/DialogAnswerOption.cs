using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AnswerOption", menuName = "Dialog Answer", order = 2)]
public class DialogAnswerOption : ScriptableObject
{
    public string text;
    public CharacterActionsController.StateChange[] characterEffects;
    public CharacterActionsController.StateChange[] npcEffects;
    public Dialog newDialogBranch;
}
