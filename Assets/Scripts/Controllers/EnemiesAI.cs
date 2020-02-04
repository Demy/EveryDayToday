using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class EnemiesAI : MonoBehaviour
{
    private List<Mob> mobs;

    private MainCharacter target;

    private float decisionTime;

    private const float decisionGap = 2f; 

    private void Start()
    {
        decisionTime = 0;
        mobs = new List<Mob>(FindObjectsOfType<Mob>());
        int size = mobs.Count;
        for (int i = 0; i < size; i++)
        {
            Mob mob = mobs[i];
            mob.AssignId(i);
            HealthPoints hp = mob.GetComponent<HealthPoints>();
            if (hp != null)
            {
                hp.OnDeath += Hp_OnDeath;
            }
        }

        target = FindObjectOfType<MainCharacter>();
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
        if (decisionTime <= 0)
        {
            decisionTime = decisionGap;
            Decide();
        }
        else
            decisionTime -= Time.fixedDeltaTime;
    }

    private void Decide()
    {
        int size = mobs.Count;
        NativeArray<int> mobIds = new NativeArray<int>(size, Allocator.TempJob);
        NativeArray<Vector3> positions = new NativeArray<Vector3>(size, Allocator.TempJob);
        NativeArray<float> sights = new NativeArray<float>(size, Allocator.TempJob);
        NativeArray<float> reaches = new NativeArray<float>(size, Allocator.TempJob);
        NativeArray<Vector3> newPositions = new NativeArray<Vector3>(size, Allocator.TempJob);
        NativeArray<Quaternion> newRotations = new NativeArray<Quaternion>(size, Allocator.TempJob);
        NativeArray<bool> hits = new NativeArray<bool>(size, Allocator.TempJob);

        for (int i = 0; i < size; i++)
        {
            Mob mob = mobs[i];
            mobIds[i] = mob.getId();
            positions[i] = mob.transform.position;
            sights[i] = mob.sigthDist;
            reaches[i] = mob.actions.weapon.reach;
        }

        EnemyDecideActionJob jobData = new EnemyDecideActionJob();
        jobData.positions = positions;
        jobData.reaches = reaches;
        jobData.sights = sights;
        jobData.mobIds = mobIds;
        jobData.target = target.transform.position;
        jobData.newPositions = newPositions;
        jobData.newRotations = newRotations;
        jobData.hits = hits;
        JobHandle handle = jobData.Schedule(size, 1);
        handle.Complete();

        int mobIndex = 0;
        for (int i = 0; i < size; i++)
        {
            if (mobs[mobIndex].getId() > i)
            {
                continue;
            }
            mobs[mobIndex].MoveAndHit(newPositions[i], newRotations[i], hits[i]);
            ++mobIndex;
        }

        mobIds.Dispose();
        positions.Dispose();
        sights.Dispose();
        reaches.Dispose();
        newPositions.Dispose();
        newRotations.Dispose();
        hits.Dispose();
    }
}
