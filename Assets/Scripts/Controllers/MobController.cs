using System;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private List<Mob> mobs;

    private float decisionTime;

    private const float decisionGap = 2f; 

    private void Start()
    {
        decisionTime = 0;
        mobs = new List<Mob>(FindObjectsOfType<Mob>());
        foreach (Mob mob in mobs)
        {
            HealthPoints hp = mob.GetComponent<HealthPoints>();
            if (hp != null)
            {
                hp.OnDeath += Hp_OnDeath;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Mob mob in mobs)
        {
            HealthPoints hp = mob.GetComponent<HealthPoints>();
            if (hp != null)
            {
                hp.OnDeath -= Hp_OnDeath;
            }
        }
    }

    void Hp_OnDeath(HealthPoints source)
    {
        mobs.Remove(source.GetComponent<Mob>());
    }


    private void FixedUpdate()
    {
        for (int i = 0; i < mobs.Count; i++)
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
