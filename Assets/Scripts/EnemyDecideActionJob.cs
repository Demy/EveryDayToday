using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public struct EnemyDecideActionJob : IJobParallelFor
{
    public NativeArray<int> mobIds;
    public NativeArray<Vector3> positions;
    public NativeArray<float> sights;
    public NativeArray<float> reaches;

    public NativeArray<Vector3> newPositions;
    public NativeArray<Quaternion> newRotations;
    public NativeArray<bool> hits;

    public Vector3 target;
    public Quaternion targetRotation;

    public void Execute(int index)
    {
        Vector3 position = positions[index];
        float sight = sights[index];
        Vector3 diff = (position - target);
        float sqrDist = diff.sqrMagnitude;
        if (sqrDist > sight * sight)
        {
            newPositions[index] = position;
            hits[index] = false;
            return;
        }
        float reach = reaches[index];
        float closeDist = (sqrDist - reach * reach) / sqrDist;
        Vector3 direction = diff * closeDist;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i == index)
            {
                continue;
            }
            Vector3 allyPos = positions[i];
            Vector3 diffAlly = (position - allyPos);
            float sqrDistAlly = diffAlly.sqrMagnitude;
            if (sqrDistAlly <= reach + reaches[i])
            {
                direction += position - allyPos;
            }
        }
        newPositions[index] = direction;
        newRotations[index] = Quaternion.LookRotation(diff);
        if (sqrDist <= reach)
        {
            hits[index] = true;
        }
    }
}
