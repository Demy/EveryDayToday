using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialog[] dialogStarters;

    public void Talk(CharacterActionsController character)
    {
        for (int i = 0; i < dialogStarters.Length; i++)
        {
            if (dialogStarters[i].TryStart(character))
            {
                break;
            }
        }
    }
}
