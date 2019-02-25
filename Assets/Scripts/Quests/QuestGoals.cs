using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoals
{ 
    public int requiredAmount;
    public int currentAmount;
    public bool isComplete;

    public void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    private void Complete()
    {
        isComplete = true;
    }

    public bool IsGoalComplete()
    {
        return isComplete;
    }
}
