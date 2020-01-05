using System.Collections.Generic;
using UnityEngine;
using static CharacterActionsController;
using static Enums;

public class NPC : MonoBehaviour
{
    public CharacterActionsController actor;
    public DialogStructure[] dialogStarters;

    public void Talk(MainCharacter character)
    {
        DialogStructure best = null;
        for (int i = 0; i < dialogStarters.Length; i++)
        {
            DialogStructure dialog = dialogStarters[i];
            bool fits = dialog.FitsRequirements(character);
            if (fits)
            {
                if (best == null || dialog.afterQuest)
                {
                    best = dialog;
                }
                if (dialog.afterQuest) break;
            }
        }
        if (best != null)
        {
            best.StartDialog(character, this);
        }
    }

    public void ApplyEffect(StateChange change)
    {
        if (actor != null)
        {
            actor.ApplyEffect(change);
        }
    }
}
