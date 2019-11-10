using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DialogPart", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public ParameterValue[] requirements;
    public string[] lines;

    private DialogView ui;
    private int currentLine;

    private void OnDestroy()
    {
        if (ui != null)
        {
            ui.OnNextLine -= DialogView_OnNextLine;
        }
    }

    public bool TryStart(CharacterActionsController character)
    {
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
        ui.Show(lines[currentLine]);
        ui.OnNextLine += DialogView_OnNextLine;
    }

    private void DialogView_OnNextLine()
    {
        if (currentLine < lines.Length)
        {
            ui.Show(lines[++currentLine]);
        } 
        else
        {
            ui.Hide();
        }
    }
}
