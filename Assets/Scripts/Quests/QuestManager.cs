using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public Quest activeQuest;
    public GameObject player;
    public List<Quest> currentQuests = new List<Quest>();
    public GameObject HudQuests;
    public GameObject signQuestAccepted;
    public GameObject signQuestCompleted;
    public UnityEvent onQuestComplete = new UnityEvent();
    public UnityEvent onQuestAccepted = new UnityEvent();
    bool signAcceptedActive = false;
    bool signCompleteActive = false; 

    private void Start()
    {
        player = gameObject; //get a ref to our player, so we can set his active quest.
        activeQuest = null;
        HudQuests.SetActive(false);
        signQuestAccepted.SetActive(false);
        signQuestCompleted.SetActive(false);
    }

    public void SetActiveQuest(Quest questToSet)
    {
        activeQuest = questToSet;
        HudQuests.SetActive(true);

        //set quest acceptedsign to active and then fade
        signQuestAccepted.SetActive(true);
        signAcceptedActive = true;
        onQuestAccepted.Invoke();
  
        
    }

    public void CloseHUDQuest()
    {
        HudQuests.SetActive(false);
    }

    public void AddQuest(Quest questToAdd)
    {
        currentQuests.Add(questToAdd);

        if(activeQuest == null || activeQuest.questName == null) //Set active quest if there isn't another active
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
            //signQuestCompleted.SetActive(true);
            //onQuestComplete.Invoke();
            Debug.Log("quest complete");
            activeQuest = null; //otherwise null the active quest.
            HudQuests.SetActive(false);
        }
    }

    //This will only increment the active quest for right now.
    public void IncrementKillQuest()
    {
        if (activeQuest == null || activeQuest.isActive == false)
            return;

        if (activeQuest.killGoals != null || activeQuest.killGoals.Length > 0)
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
        if(activeQuest.IsQuestComplete() && activeQuest.questCompleteCondition != CompleteCondition.TURN_IN)
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

    //Handle turn in
    public void CompleteTurnInQuest(Quest quest)
    {
        if (!quest.isComplete)
            return;

        GiveRewards();
   
        QuestCompleted(quest);
    }

    //Handle Kill target
    public void CompleteTargetKillQuest(Quest quest)
    {

   
        if (!quest.isComplete)
            return;

        GiveRewards();
      
        QuestCompleted(quest);
    }
}
