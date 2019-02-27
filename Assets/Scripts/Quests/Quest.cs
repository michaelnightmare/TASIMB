using System.Collections;
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

    public void CheckGoals()
    {
        //If we have item collection quests or whatever, will need a new array for each and loop through them.
        foreach(KillGoal quest in killGoals)
        {
            if (!quest.IsGoalComplete())
                return; //Bail out if any of the goals aren't complete
        }

        Complete();
    }

    private void Complete()
    {
        isComplete = true;
        Debug.Log("Quest Complete: " + questName);
    }

    public bool IsQuestComplete()
    {
        return isComplete;
    }
}

public enum CompleteCondition
{
    TURN_IN,
    AUTO_COMPLETE
}
