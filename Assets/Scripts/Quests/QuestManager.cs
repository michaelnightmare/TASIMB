using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest activeQuest;
    public GameObject player;
    private List<Quest> currentQuests = new List<Quest>();

    private void Start()
    {
        player = gameObject; //get a ref to our player, so we can set his active quest.
        activeQuest = null;
    }

    public void SetActiveQuest(Quest questToSet)
    {
        activeQuest = questToSet;
    }

    public void AddQuest(Quest questToAdd)
    {
        currentQuests.Add(questToAdd);

        if(activeQuest.questName == null) //Set active quest if there isn't another active
        {
            SetActiveQuest(questToAdd);
        }
    }

    public void QuestCompleted(Quest completedQuest)
    {
        currentQuests.Remove(completedQuest);

        Debug.Log("Quest Completed and to be cleared");

        if(currentQuests.Count > 0)
        {
            activeQuest = currentQuests[0]; // set active quest to the first in the list.
        }
        else
        {
            activeQuest = null; //otherwise null the active quest.
        }
    }

    //This will only increment the active quest for right now.
    public void IncrementKillQuest()
    {
        if (activeQuest == null)
            return;

        if (activeQuest.killGoals.Length > 0)
        {
            for (int i = 0; i < activeQuest.killGoals.Length; i++)
            {
                activeQuest.killGoals[i].EnemyDeath();
            }
        }

        activeQuest.CheckGoals(); //After incrementing kills check the quest
        ClearQuestIfNecessary();
    }

    public void ClearQuestIfNecessary()
    {
        //If we've satisfied all quest goals, give the rewards and clear the quest

        //This will require more work if we want the player to have to go talk to the NPC again to complete.
        if(activeQuest.IsQuestComplete())
        {
            GiveRewards();
            QuestCompleted(activeQuest);
        }
    }

    public void GiveRewards()
    {
        player.GetComponent<PlayerScript>().gold += activeQuest.goldReward;
        Debug.Log("Player receives " + activeQuest.goldReward + " gold!");
    }
}
