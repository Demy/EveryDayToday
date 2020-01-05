using System;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private Mob[] mobs;

    private float decisionTime;

    private const float decisionGap = 2f; 

    private void Start()
    {
        decisionTime = 0;
        mobs = FindObjectsOfType<Mob>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            Mob mob = mobs[i];
            if (decisionTime <= 0)
                mob.Decide();
            mob.MakeAction();
        }
        if (decisionTime <= 0)
            decisionTime = decisionGap;
        else
            decisionTime -= Time.fixedDeltaTime;
    }
}
