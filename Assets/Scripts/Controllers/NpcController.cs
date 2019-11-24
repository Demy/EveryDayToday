using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour
{
    public float talkRange;

    private float sqrTalkRange;
    private NPC[] npcs;

    void Start()
    {
        npcs = FindObjectsOfType<NPC>();
    }

    public NPC GetClosest(CharacterActionsController character, float dist)
    {
        float bestSqrDist = dist * dist;
        NPC closest = null;
        for (int i = 0; i < npcs.Length; i++)
        {
            float sqrDist = (npcs[i].transform.position - character.transform.position).sqrMagnitude;
            if (sqrDist <= bestSqrDist)
            {
                bestSqrDist = sqrDist;
                closest = npcs[i];
            }
        }
        return closest;
    }
}
