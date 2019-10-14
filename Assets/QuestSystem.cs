using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Objective
{
    [Header("Options")]
    public string questName;
    [TextArea] public string questText;

    public enum CompletionCriteria {Dialog, Kill, Location}

    [Header("References")]
    public QuestObjectiveHUD hud;
    public GameObject icon;

    [Header("Completion")]
    public GameObject dialogTarget;
    public GameObject killTarget;
    public GameObject locationTarget;
    public CompletionCriteria type;

    [Header("Events")]
    public UnityEvent OnObjectiveUnlocked = new UnityEvent();
    public UnityEvent OnObjectiveCompleted = new UnityEvent();

    public bool CheckComplete(CompletionCriteria action, GameObject target)
    {
        if(action == type)
        {
            if(type == CompletionCriteria.Dialog)
            {
                if(dialogTarget == target)
                {
                    return true;
                }
            }
            else if(type == CompletionCriteria.Kill)
            {
                if (killTarget == target)
                {
                    return true;
                }
            }
            else if(type == CompletionCriteria.Location)
            {
                if (locationTarget == target)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void OnGiveQuest()
    {
        OnObjectiveUnlocked.Invoke();
        icon.SetActive(true);
    }

    public void OnCompleteQuest()
    {
        Debug.Log("Completed Quest: " + questName);
        OnObjectiveCompleted.Invoke();
        icon.SetActive(false);
    }
}

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem quests;

    [Header("References")]
    public QuestObjectiveHUD objectiveHUD;


    public List<Objective> objectives;
    public int startingObjective = 0;
    public int currentObjective = -1;


    private void Awake()
    {
        if(quests == null)
        {
            quests = this;
        }
        else if(quests != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        currentObjective = startingObjective;
        GiveQuest(currentObjective);
    }

    public void GiveNextQuest()
    {
        int newObjective = Mathf.Clamp(currentObjective + 1, 0, objectives.Count - 1);
        if(newObjective != currentObjective)
        {
            GiveQuest(newObjective);
        }
    }

    public void GiveQuest(int index)
    {
        currentObjective = index;
        objectives[currentObjective].OnGiveQuest();
        objectiveHUD.ReceivedNewQuest(objectives[currentObjective], currentObjective == 0);
    }

    public void CompleteCurrentQuest()
    {
        objectives[currentObjective].OnCompleteQuest();
    }

    public void NotifyDialog(GameObject target)
    {
        if (objectives[currentObjective].CheckComplete(Objective.CompletionCriteria.Dialog, target))
        {
            CompleteCurrentQuest();
            GiveNextQuest();
        }
    }

    public void NotifyKill(GameObject target)
    {
        Debug.Log(target.name);
        if (objectives[currentObjective].CheckComplete(Objective.CompletionCriteria.Kill, target))
        {
            CompleteCurrentQuest();
            GiveNextQuest();
        }
    }

    public void NotifyArrivedAtLocation(GameObject target)
    {
        if (objectives[currentObjective].CheckComplete(Objective.CompletionCriteria.Location, target))
        {
            CompleteCurrentQuest();
            GiveNextQuest();
        }
    }

    
}
