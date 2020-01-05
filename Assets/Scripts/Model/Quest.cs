using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static Enums;

public class Quest
{
    public delegate void Success(Quest quest);
    public Success OnSuccess = (i) => { };
    public delegate void Fail(Quest quest, Requirement failed, int value);
    public Fail OnFail = (i, j, k) => { };

    private QuestStructure structure;

    private Dictionary<Requirement, int> successValues = new Dictionary<Requirement, int>();
    private Dictionary<Requirement, int> failValues = new Dictionary<Requirement, int>();

    public Quest(QuestStructure structure)
    {
        this.structure = structure;
        int i;
        for (i = 0; i < structure.successRequirements.Length; i++)
        {
            successValues.Add(structure.successRequirements[i], 0);
        }
        for (i = 0; i < structure.failRequirements.Length; i++)
        {
            failValues.Add(structure.failRequirements[i], 0);
        }
    }

    public void TryIncrement(Enums.Flag flag, int v)
    {
        int i;
        bool succeed = false;
        for (i = 0; i < structure.successRequirements.Length; i++)
        {
            CollectableRequirement collectable = structure.successRequirements[i] as CollectableRequirement;
            if (collectable != null && collectable.flag == flag)
            {
                ++successValues[collectable];
                if (successValues[collectable] >= collectable.GetValue())
                {
                    succeed = true;
                    break;
                }
            }
        }
        for (i = 0; i < structure.failRequirements.Length; i++)
        {
            CollectableRequirement collectable = structure.failRequirements[i] as CollectableRequirement;
            if (collectable != null && collectable.flag == flag)
            {
                ++failValues[collectable];
                if (failValues[collectable] >= collectable.GetValue())
                {
                    OnFail(this, collectable, failValues[collectable]);
                    return;
                }
            }
        }
        if (succeed)
        {
            OnSuccess(this);
        }
    }

    public String GetQuestId()
    {
        return structure.questId;
    }

    public void GrantReward(CharacterActionsController character)
    {
        foreach (StateChange reward in structure.rewards)
        {
            switch (reward.type)
            {
                case StateChangeType.HP:
                    character.hp.Change((int)reward.value);
                    return;
            }
        }
    }
}
