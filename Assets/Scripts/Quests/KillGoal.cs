using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillGoal : QuestGoals
{ 
    public void EnemyDeath()
    {
        this.currentAmount++;
        Evaluate();
    }
}
