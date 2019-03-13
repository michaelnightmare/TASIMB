﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject player;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;

    //Edited out until we make a UI screen for the quest
    public void Start()
    {
        //goldText.text = quest.goldReward.ToString(); uncomment later
    }

    // This whole script will need adjusting etc if we decide to have quests that unlock after certain conditions are met.
    // For now the QuestGiver may only have one quest.
    public void GiveQuest()
    {
        if (quest.isActive || quest.isComplete)
            return; //Dont give quest if its already active or completed

        titleText.text = quest.questName;
        descriptionText.text = quest.questDescription;

        quest.isActive = true;
        player.GetComponent<QuestManager>().AddQuest(quest);
        Debug.Log("Quest Recieved");
    }

    public void CompleteQuestForPlayer() //This would be for turnin quests only
    {
        for (int i = 0; i < player.GetComponent<QuestManager>().currentQuests.Count; i++)
        {
            Quest currentQuest = player.GetComponent<QuestManager>().currentQuests[i];
            if (currentQuest.isComplete && currentQuest.questCompleteCondition == CompleteCondition.TURN_IN && this.gameObject == currentQuest.turnInPoint)
            {
                player.GetComponent<QuestManager>().CompleteTurnInQuest(currentQuest);
            }
            else if(currentQuest.isComplete && currentQuest.questCompleteCondition == CompleteCondition.KILL_BOSS)
            {
                player.GetComponent<QuestManager>().CompleteTargetKillQuest(currentQuest);
            }
        }
    }

    public void MarkKillAsComplete()
    {
        quest.KillComplete();
        CompleteQuestForPlayer();
    }
}
