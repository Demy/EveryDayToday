using UnityEngine;

[CreateAssetMenu(fileName = "DialogPart", menuName = "Dialog/Dialog", order = 1)]
public class DialogStructure : ScriptableObject
{
    public ParameterValue[] requirements;
    public string[] lines;
    public DialogAnswerOption[] answers;
    public bool afterQuest;
    public QuestStructure quest;
    public bool questSucceed;

    private DialogView ui;
    private int currentLine;
    private MainCharacter speakingCharacter;
    private NPC speakingNpc;

    private void OnDestroy()
    {
        if (ui != null)
        {
            ui.OnNextLine -= DialogView_OnNextLine;
        }
    }

    public void StartDialog(MainCharacter character, NPC npc)
    {
        speakingCharacter = character;
        speakingNpc = npc;
    
        StartDialog();
    }

    public bool FitsRequirements(MainCharacter character)
    {
        int i;
        for (i = 0; i < requirements.Length; i++)
        {
            if (!requirements[i].Check(character))
            {
                return false;
            }
        }
        for (i = 0; i < answers.Length; i++)
        {
            QuestStructure questStructure = answers[i].questToStart;
            if (questStructure != null)
            {
                if (character.HasQuest(questStructure.questId) || 
                    character.FailedQuest(questStructure.questId) || 
                    character.SucceedQuest(questStructure.questId))
                return false;
            }
        }
        if (afterQuest)
        {
            if (questSucceed && !character.SucceedQuest(quest.questId))
            {
                return false;
            }
            if (!questSucceed && !character.FailedQuest(quest.questId))
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

        if (afterQuest)
        {
            if (questSucceed) speakingCharacter.GetReward(quest.questId);
            else speakingCharacter.Fail(quest.questId);
        }
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
        if (answer.newDialogBranch != null && answer.newDialogBranch.FitsRequirements(speakingCharacter))
        {
            answer.newDialogBranch.StartDialog(speakingCharacter, speakingNpc);
        }
        else
        {
            ui.Hide();
        }
        if (answer.questToStart != null)
        {
            speakingCharacter.StartQuest(answer.questToStart);
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
