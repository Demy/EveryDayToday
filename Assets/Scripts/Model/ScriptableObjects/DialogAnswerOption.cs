using UnityEngine;
using System.Collections;
using static Enums;

[CreateAssetMenu(fileName = "AnswerOption", menuName = "Dialog/Dialog Answer", order = 2)]
public class DialogAnswerOption : ScriptableObject
{
    public string text;
    public StateChange[] characterEffects;
    public StateChange[] npcEffects;
    public DialogStructure newDialogBranch;
    public QuestStructure questToStart;
}
