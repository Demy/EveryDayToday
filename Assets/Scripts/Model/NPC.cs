using UnityEngine;
using static CharacterActionsController;

public class NPC : MonoBehaviour
{
    public CharacterActionsController actor;
    public Dialog[] dialogStarters;

    public void Talk(CharacterActionsController character)
    {
        for (int i = 0; i < dialogStarters.Length; i++)
        {
            if (dialogStarters[i].TryStart(character, this))
            {
                break;
            }
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
