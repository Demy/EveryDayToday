using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "DialogPart", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public ParameterValue[] requirements;
    public string[] lines;
    public DialogAnswerOption[] answers;

    private DialogView ui;
    private int currentLine;
    private CharacterActionsController speakingCharacter;
    private NPC speakingNpc;

    private void OnDestroy()
    {
        if (ui != null)
        {
            ui.OnNextLine -= DialogView_OnNextLine;
        }
    }

    public bool TryStart(CharacterActionsController character, NPC npc)
    {
        speakingCharacter = character;
        speakingNpc = npc;

        if (FitsRequirements(character))
        {
            StartDialog();
            return true;
        }
        return false;
    }

    private bool FitsRequirements(CharacterActionsController character)
    {
        for (int i = 0; i < requirements.Length; i++)
        {
            if (!requirements[i].Check(character))
            {
                return false;
            }
        }
        return true;
    }

    private void StartDialog()
    {
        currentLine = 0;
        if (ui == null)
        {
            ui = FindObjectOfType<DialogView>();
        }
        if (lines.Length == 0)
        {
            ui.Hide();
            return;
        }
        ui.Show(lines[currentLine]);
        ui.OnNextLine += DialogView_OnNextLine;
    }

    private void DialogView_OnNextLine()
    {
        if (++currentLine < lines.Length)
        {
            ui.Show(lines[currentLine]);
        } 
        else
        {
            ui.OnNextLine -= DialogView_OnNextLine;
            if (answers.Length > 0)
            {
                ui.ShowAnswerOptions(answers);
                ui.OnAnswer += DialogView_OnAnswer;
            }
            else
            {
                ui.Hide();
            }
        }
    }

    private void DialogView_OnAnswer(DialogAnswerOption answer)
    {
        ui.OnAnswer -= DialogView_OnAnswer;
        if (answer.newDialogBranch != null)
        {
            answer.newDialogBranch.TryStart(speakingCharacter, speakingNpc);
        }
        else
        {
            ui.Hide();
        }
        int i;
        for (i = 0;  i < answer.characterEffects.Length; i++)
        {
            speakingCharacter.ApplyEffect(answer.characterEffects[i]);
        }
        for (i = 0; i < answer.npcEffects.Length; i++)
        {
            speakingNpc.ApplyEffect(answer.npcEffects[i]);
        }
    }
}
