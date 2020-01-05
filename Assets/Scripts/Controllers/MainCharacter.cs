using System;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : CharacterActionsController
{
    private List<Quest> currentQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    private List<Quest> failedQuests = new List<Quest>();

    private void Start()
    {
        if (weapon != null)
        {
            weapon.OnKill += Weapon_OnKill;
        }
        hp.OnChange += Hp_OnChange;
    }

    private void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.OnKill -= Weapon_OnKill;
        }
        hp.OnChange -= Hp_OnChange;
        currentQuests.ForEach((Quest quest) =>
        {
            quest.OnSuccess -= Quest_OnSuccess;
            quest.OnFail -= Quest_OnFail;
        });
    }

    public void StartQuest(QuestStructure questToStart)
    {
        Quest quest = new Quest(questToStart);
        currentQuests.Add(quest);
        quest.OnSuccess += Quest_OnSuccess;
        quest.OnFail += Quest_OnFail;
    }

    private int FindQuest(string questId, List<Quest> quests)
    {
        int index = quests.FindIndex((Quest quest) => quest.GetQuestId() == questId);
        return index;
    }

    public bool HasQuest(string questId)
    {
        return FindQuest(questId, currentQuests) >= 0;
    }

    public bool SucceedQuest(string questId)
    {
        return FindQuest(questId, completedQuests) >= 0;
    }

    public bool FailedQuest(string questId)
    {
        return FindQuest(questId, failedQuests) >= 0;
    }

    public void GetReward(string questId)
    {
        int questIndex = FindQuest(questId, completedQuests);
        if (questIndex >= 0)
        {
            completedQuests[questIndex].GrantReward(this);
            completedQuests.RemoveAt(questIndex);
        }
    }

    public void Fail(string questId)
    {
        int questIndex = FindQuest(questId, failedQuests);
        if (questIndex >= 0)
        {
            failedQuests.RemoveAt(questIndex);
        }
    }

    private void Quest_OnFail(Quest quest, Requirement failed, int value)
    {
        RemoveQuest(quest);
        failedQuests.Add(quest);
    }

    private void Quest_OnSuccess(Quest quest)
    {
        RemoveQuest(quest);
        completedQuests.Add(quest);
    }

    private void RemoveQuest(Quest quest)
    {
        currentQuests.Remove(quest);
        quest.OnSuccess -= Quest_OnSuccess;
        quest.OnFail -= Quest_OnFail;
    }

    void Hp_OnChange(int value, int current)
    {
        currentQuests.ForEach((Quest quest) =>
        {
            quest.TryIncrement(Enums.Flag.Hp, value);
        });
    }

    private void Weapon_OnKill(CharacterActionsController target)
    {
        Quest[] quests = new Quest[currentQuests.Count];
        currentQuests.CopyTo(quests);
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].TryIncrement(Enums.Flag.Monaster, 1);
        }
    }
}
