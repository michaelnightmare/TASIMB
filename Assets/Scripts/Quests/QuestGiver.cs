//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestGiver : MonoBehaviour
//{


//    public Quest quest;
//    public GameObject player;
//    public Text titleText;
//    public Text descriptionText;
//    public Text goldText;
//    public bool activeOnStart = false;


//    public void Start()
//    {
//        Debug.Log(quest);
//        if (quest.questIcon == null)
//        {
//            Transform iconT = transform.Find("GoalTracker");
//            if (iconT != null)
//            {
//                quest.questIcon = iconT.gameObject;
//            }
//            else
//            {
//                Debug.Log(gameObject);
//            }
//        }


//        goldText.text = quest.goldReward.ToString();
//        if (activeOnStart)
//        {
//            GiveQuest();
//        }
//        else
//        {
//            quest.DeactivateQuest();
//        }
//    }


//    public void GiveQuest()
//    {
//        if (quest.isActive || quest.isComplete)
//            return; //Dont give quest if its already active or completed

//        titleText.text = quest.questName;
//        descriptionText.text = quest.questDescription;

//        quest.ActivateQuest();
//        player.GetComponent<QuestManager>().AddQuest(quest);

//        Debug.Log("Quest Recieved");
//    }

//    public void CompleteQuestForPlayer() //This would be for turnin quests only
//    {
//        for (int i = 0; i < player.GetComponent<QuestManager>().currentQuests.Count; i++)
//        {
//            Quest currentQuest = player.GetComponent<QuestManager>().currentQuests[i];
//            if (currentQuest.isComplete && currentQuest.questCompleteCondition == CompleteCondition.TURN_IN && this.gameObject == currentQuest.turnInPoint)
//            {
//                player.GetComponent<QuestManager>().CompleteTurnInQuest(currentQuest);

//            }
//            else if (currentQuest.isComplete && currentQuest.questCompleteCondition == CompleteCondition.KILL_BOSS)
//            {
//                player.GetComponent<QuestManager>().CompleteTargetKillQuest(currentQuest);
//            }
//        }

//        quest.Complete();
//    }

//    public void MarkKillAsComplete()
//    {
//        quest.KillComplete();

//        CompleteQuestForPlayer();
//    }


//}
