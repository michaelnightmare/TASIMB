﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string questName;
    public string questDescription;
    public KillGoal[] killGoals;
    public int goldReward;
    public bool isComplete;
    public CompleteCondition questCompleteCondition;
    public GameObject turnInPoint;
    public GameObject killTarget;
    public GameObject questIcon;

    private bool isTargetDead;

    public void DeactivateQuest()
    {
        HideGoalTracker();
        isActive = false;
    }

    public void ActivateQuest()
    {
        isActive = true;
        questIcon.SetActive(true);
    }

    public void CheckGoals()
    {
        //If we have item collection quests or whatever, will need a new array for each and loop through them.
        foreach(KillGoal goal in killGoals)
        {
            if (!goal.IsGoalComplete())
                return; //Bail out if any of the goals aren't complete
        }

        if (questCompleteCondition == CompleteCondition.KILL_BOSS && !isTargetDead)
            return; //Don't allow a complete if there is a kill target

        Complete();
    }

    public void Complete()
    {
        isComplete = true;
        HideGoalTracker();
        Debug.Log("Quest Complete: " + questName);
    }

    public bool IsQuestComplete()
    {
        return isComplete;
    }

    public void KillComplete()
    {
        isTargetDead = true;
        CheckGoals();
    }

    public void HideGoalTracker()
    {
        questIcon.SetActive(false);


    }
}

public enum CompleteCondition
{
    TURN_IN,
    AUTO_COMPLETE,
    KILL_BOSS
}
